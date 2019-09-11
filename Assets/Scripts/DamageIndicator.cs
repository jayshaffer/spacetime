using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageIndicator : MonoBehaviour
{
    public float fadeRate;
    TextMesh textMesh;
    Queue hitQueue;
    void Start()
    {
       hitQueue = new Queue();
       textMesh = GetComponent<TextMesh>(); 
       SetOpacity(0);
       StartCoroutine("ShowHitQueue");
    }

    void Update()
    {
        
    }

    public void Show(string text){
        SetOpacity(1);
        textMesh.text = text; 
        StartCoroutine("Fade");
    }

   public void DisplayHit(float amount){
       string indicator = "";
        if (amount > 0)
        {
            indicator += "-";
        }
        indicator += Math.Abs(amount);
        if(amount > 0){
            textMesh.color = Color.red;
        }
        else{
            textMesh.color = Color.green;
        }
        hitQueue.Enqueue(indicator);
    }

    void SetOpacity(float value){
        Color color = textMesh.color;        
        color.a = value;
        textMesh.color = color;
    }

    IEnumerator ShowHitQueue(){
        while(true){
            if(hitQueue.Count != 0){
                Show(hitQueue.Dequeue().ToString());
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator Fade(){
        float opacity = 1;
        while(textMesh.color.a > 0){
            SetOpacity(opacity-=.1f);
            yield return new WaitForSeconds(.1f);
        }
        StopCoroutine("Fade");
    }
}
