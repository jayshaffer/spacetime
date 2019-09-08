using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject timerBox;
    public float time;
    public bool invincible = false;
    public float invTime = 0;
    TextMesh timerText;
    Rigidbody2D rb;
    float lastHit = 0;
    bool dead = false;
    GameController gameController;
    

    public void Hit(float amount)
    {
        if (lastHit == 0 || lastHit + invTime < Time.time)
        {
            time -= amount;
            lastHit = Time.time;
        }
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

    void Update(){
        if(time < 0){
            Kill();
        }
    }

    void FixedUpdate()
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        float horizontal = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * speed * Time.fixedDeltaTime;
        rb.velocity = new Vector3(horizontal, vertical, transform.position.z);
        timerText.text = minutes + ":" + seconds;
    }

    IEnumerator StartCountdown(float value)
    {
        while (!dead)
        {
            time--;
            yield return new WaitForSeconds(1.0f);
        }
    }

    void Kill(){
       gameController.RestartScene(); 
    }
}
