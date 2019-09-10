using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenuUI;
    public GameController gameController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape)){
           if(gameController.isPaused){
               Resume();
           }else{
               Pause();
           }
       } 
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameController.isPaused = false;
    }
    
    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameController.isPaused = true;
    }

    public void QuitGame(){
        Application.Quit();
    }
}
