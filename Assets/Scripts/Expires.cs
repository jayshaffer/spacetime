using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expires : MonoBehaviour
{
    public float ttl;
    void Start()
    {
        Destroy(gameObject, ttl);
    }

    void Update()
    {
        
    }
}
