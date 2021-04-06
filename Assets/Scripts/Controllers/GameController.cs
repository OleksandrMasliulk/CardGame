using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController Instance { get; private set; }

    public Animator transition;
    public GameObject donePanel;
    public GameObject lostPanel;

    private void Awake() {
        Instance = this;
    }

    private void Start() {

    }

    public void NextLevel()
    {
        CharacterTurnController.Instance.StopAllCoroutines();

        transition.SetTrigger("Fadeout");

        StartCoroutine(LoadNext());
    }

    IEnumerator LoadNext()
    {
        yield return new WaitForSeconds(transition.GetCurrentAnimatorStateInfo(0).length);

        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        try
        {
            SceneManager.LoadScene(nextLevel);
        }
        catch
        {
            Debug.LogError(this);
        }
    }

    public void LevelDone()
    {
        SaveLoadSystem.Save();

        CharacterTurnController.Instance.StopAllCoroutines();

        donePanel.SetActive(true);
    }

    public void Lost()
    {
        SaveLoadSystem.Save();

        CharacterTurnController.Instance.StopAllCoroutines();

        lostPanel.SetActive(true);
    }

    public void ToVillage()
    {
        Destroy(PlayerParams.Instance.gameObject);
        SceneManager.LoadScene(1);
    }
}
