using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    public float time;
    public bool invincible = false;
    public float invTime = 0;
    float lastHit = 0;
    Queue hitQueue;
    void Start()
    {
        hitQueue = new Queue();
    }

    void Update()
    {

    }

    public float GetHitQueueNext(){
        if(hitQueue.Count > 0){
            return (float) hitQueue.Dequeue();
        }
        return 0;
    }

    public bool Hit(float damage)
    {
        if (lastHit == 0 || lastHit + invTime < Time.time)
        {
            hitQueue.Enqueue(damage);
            time -= damage;
            lastHit = Time.time;
            return true;
        }
        return false;
    }
}
