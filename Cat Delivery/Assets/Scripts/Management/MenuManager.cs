using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu(){
        SceneManager.LoadScene(Scenes.MENU);
    }

    public void ExitApplication(){
        Application.Quit();
    }
}
