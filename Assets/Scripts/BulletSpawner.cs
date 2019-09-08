using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public float delay;
    public bool autoFire = false;
    public GameObject bullet;
    float lastShot;
    

    void Start()
    {
        lastShot = Time.time;
    }

    void Update()
    {
        if (autoFire && lastShot + delay < Time.time)
        {
            Fire();
        }
    }

    public void Fire()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        lastShot = Time.time;
    }
}
