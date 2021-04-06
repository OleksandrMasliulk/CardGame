using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        Debug.Log("Settings");
    }

    public void Quit()
    {
        Application.Quit();
    }
}


