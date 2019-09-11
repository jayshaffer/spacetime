using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject timerBox;
    public float maxSpeed;
    public GameObject damageIndicator;
    public GameObject gun;
    public GameObject bulletSpawner;
    public Hurtable hurtable;
    public float shotCost;
    public ParticleSystem explosion;
    public GameObject sprite;
    TextMesh damageText;
    TextMesh timerText;
    Rigidbody2D rb;
    bool dead = false;
    GameController gameController;

    public void Hit(float amount)
    {
        damageIndicator.GetComponent<DamageIndicator>().DisplayHit(amount);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timerText = timerBox.GetComponent<TextMesh>();
        StartCoroutine("StartCountdown", 1);
        gameController = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();
    }

    void Fire()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPos = pos - gun.transform.position;
        newPos.z = 0;
        gun.transform.right = newPos;
        bulletSpawner.GetComponent<BulletSpawner>().Fire();
        hurtable.Hit(shotCost, true);
    }

    void Update()
    {
        if(dead){
            return;
        }
        if(gameController.isPaused){
            return;
        }
        if (hurtable.time < 0)
        {
            StartCoroutine("Kill");
        }
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        float latestHit = hurtable.GetHitQueueNext();
        if(latestHit != 0){
            Hit(latestHit);
        }
    }

    void FixedUpdate()
    {
        if(dead){
            return;
        }
        float minutes = Mathf.Floor(hurtable.time / 60);
        float seconds = Mathf.RoundToInt(hurtable.time % 60);
        float horizontal = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * speed * Time.fixedDeltaTime;
        rb.AddForce(transform.right * speed * horizontal);
        rb.AddForce(transform.up * speed * vertical);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        string buffer = ":";
        if(seconds < 10){
            buffer += "0";
        }
        if(hurtable.time < 0){
            timerText.text = "0:00";
        }
        else{
            timerText.text = minutes + buffer + seconds;
        }
    }

    IEnumerator StartCountdown(float value)
    {
        while (!dead)
        {
            hurtable.time--;
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator Kill()
    {
        StartCoroutine("FlashCounter");
        gameController.GameOver();
        explosion.Play();
        dead = true;
        sprite.SetActive(false);
        rb.velocity = new Vector3(0,0,0);
        yield return new WaitForSeconds(5.0f);
        gameController.RestartScene();
        yield return null;
    }

    IEnumerator FlashCounter(){
        while(true){
            timerBox.SetActive(!timerBox.activeSelf);
            yield return new WaitForSeconds(.5f);
        }
    }
}
