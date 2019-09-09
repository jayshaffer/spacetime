using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    public float time;
    public bool invincible = false;
    public float invTime = 0;
    float lastHit = 0;
    void Start()
    {
    }

    void Update()
    {

    }

    public bool Hit(float damage)
    {
        if (lastHit == 0 || lastHit + invTime < Time.time)
        {
            time -= damage;
            lastHit = Time.time;
            return true;
        }
        return false;
    }
}
