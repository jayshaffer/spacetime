using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public Rigidbody2D rb;
    void Start()
    {
       rb.velocity = transform.right * speed; 
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            PlayerController player = other.gameObject.GetComponent<PlayerController>();    
            player.Hit(damage);
        }
    }
}
