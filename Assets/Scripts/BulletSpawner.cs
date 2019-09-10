using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public float delay;
    public bool autoFire = false;
    public GameObject bullet;
    public LayerMask mask;
    public float damage;
    public Material material;
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
        Bullet spawnedBullet = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Bullet>();
        spawnedBullet.damage = damage;
        spawnedBullet.mask = mask;
        lastShot = Time.time;
    }
}
