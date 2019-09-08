using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Transform playerTransform;
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
       playerTransform = player.GetComponent<Transform>(); 
    }

    void Update()
    {
        Vector3 newVec = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z); 
        transform.position = newVec;    
    }
}
