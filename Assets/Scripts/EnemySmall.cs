using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmall : MonoBehaviour
{

    public float followDistance;
    Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
    }

    void Update()
    {
    }
}
