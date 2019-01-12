using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public enum Scenes { themes = 1, mainGame = 2 }


    public void GoToScene(Scenes s)
    {
        switch (s)
        {
            case Scenes.themes:
                SceneManager.LoadScene("Themes");
                break;
            case Scenes.mainGame:
                SceneManager.LoadScene("MainGame");

                break;
        }

    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Themes");
    }
}
