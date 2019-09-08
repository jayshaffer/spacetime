using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEverything : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("here");
       Destroy(other.gameObject); 
    }
}
