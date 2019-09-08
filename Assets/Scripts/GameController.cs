using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text timerText;
    float time;
    bool gameOver = false;
    void Start()
    {
        time = 0;
        StartCoroutine("StartTimer");
    }

    void Update()
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        timerText.text = minutes + ":" + seconds;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator StartTimer()
    {
        while (!gameOver)
        {
            time++;
            yield return new WaitForSeconds(1.0f);
        }
    }

}
