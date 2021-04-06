using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VillageUI : MonoBehaviour
{
    public Button ruinsButton;
    public Button foothillsButton;

    private void Awake()
    {
        SaveLoadSystem.Load();
    }

    private void Start()
    {
        if (PlayerProgress.isForestFinished)
            ruinsButton.interactable = true;
        if (PlayerProgress.isRuinsFinished)
            foothillsButton.interactable = true;
    }

    public void PlayLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
