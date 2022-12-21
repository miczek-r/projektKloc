using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class DeathScene : MonoBehaviour
{
    void Start(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void PlayGame (){
        SceneManager.LoadScene(1);
    }

    public void QuitGame (){
        Debug.Log("QUIT!");
        Application.Quit();    
    }
    public void FullScreen(bool is_fullscene){
        Screen.fullScreen = is_fullscene;
    }
}
