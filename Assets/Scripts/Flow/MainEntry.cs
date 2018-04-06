using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntry : MonoBehaviour
{


    //deriger vers le main scene 
    public void LoadMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    //deriger vers le Game over
    public void LoadGameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }


    public void LoadEndGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");
    }

}
