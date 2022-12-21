using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;

    [SerializeField] GameObject pauseMenu;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(isGamePaused){
                ResumeGame();
            }
            else{
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                PauseGame();
            }
        }
    }

    public void ResumeGame(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void PauseGame(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void QuitGame (){
        Debug.Log("QUIT!");
        Application.Quit();    
    }
}
