using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public float fadeRate;
    TextMesh textMesh;
    void Start()
    {
       textMesh = GetComponent<TextMesh>(); 
       SetOpacity(0);
    }

    void Update()
    {
        
    }

    public void Show(string text){
        SetOpacity(1);
        textMesh.text = text; 
        Debug.Log("show");
        StartCoroutine("Fade");
    }

    void SetOpacity(float value){
        Color color = textMesh.color;        
        color.a = value;
        textMesh.color = color;
    }

    IEnumerator Fade(){
        float opacity = 1;
        while(textMesh.color.a > 0){
            SetOpacity(opacity-=.1f);
            yield return new WaitForSeconds(.1f);
        }
    }
}
