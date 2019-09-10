using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject timerBox;
    public float maxSpeed;
    public GameObject damageIndicator;
    public GameObject gun;
    public GameObject bulletSpawner;
    public Hurtable hurtable;
    TextMesh damageText;
    TextMesh timerText;
    Rigidbody2D rb;
    bool dead = false;
    GameController gameController;

    public void Hit(float amount)
    {
        string indicator = "";
        if (amount > 0)
        {
            indicator += "-";
        }
        indicator += amount;
        damageIndicator.GetComponent<DamageIndicator>().Show(indicator);
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
    }

    void Update()
    {
        if (hurtable.time < 0)
        {
            Kill();
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
        timerText.text = minutes + ":" + seconds;
    }

    IEnumerator StartCountdown(float value)
    {
        while (!dead)
        {
            hurtable.time--;
            yield return new WaitForSeconds(1.0f);
        }
    }

    void Kill()
    {
        gameController.RestartScene();
    }
}
