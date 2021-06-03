using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()//plays the first level
    {
        SceneManager.LoadScene("GameScene");//scenemanager will load the scene 
    }
    public void Quit()//to quit the game
    {
        Debug.Log("Quit");//if the player has successfully exited from the game
        Application.Quit();//quitting from the game application
    }
}
