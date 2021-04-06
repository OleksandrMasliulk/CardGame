using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    public Level level;

    public Image background;
    public Image foreground;

    public GameObject playerSlot;
    public PlayerCharacter playerObject;

    public GameObject[] enemySlots;
    public GameObject[] allySlots;
    
    public GameObject npcCharacterPrefab;
    public GameObject playerPrefab;

    public Animator transition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        ClearLevel();

        background.sprite = level.backgroundImage;
        foreground.sprite = level.foregroundImage;

        int layersOffsetEnemy = GetMaxLayers(level.enemies);
        int layersOffsetAllies = GetMaxLayers(level.allies);

        if (PlayerParams.Instance == null)
        {
            GameObject newPlayer = Instantiate(playerPrefab, playerSlot.transform.position, Quaternion.identity);
            PlayerParams player = newPlayer.GetComponent<PlayerParams>();

            player.Init(playerObject, layersOffsetAllies * 2);
        }
        else
        {
            PlayerParams.Instance.NewLevelInit();
        }

        for (int i = 0; i < level.enemies.Length; i++)
        {
            GameObject newEnemy = Instantiate(npcCharacterPrefab, enemySlots[enemySlots.Length - i - 1].transform.position, Quaternion.identity);
            NPCParams character = newEnemy.GetComponent<NPCParams>();

            character.Init(level.enemies[i], layersOffsetEnemy * (enemySlots.Length - i - 1));
        }
        for (int i = 0; i < level.allies.Length; i++)
        {
            GameObject newAlly = Instantiate(npcCharacterPrefab, allySlots[allySlots.Length - i - 1].transform.position, Quaternion.identity);
            NPCParams character = newAlly.GetComponent<NPCParams>();

            character.Init(level.allies[i], layersOffsetAllies * (allySlots.Length - i - 1));
        }

        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(transition.GetCurrentAnimatorStateInfo(0).length);

        CharacterTurnController.Instance.NextTurn();
    }

    public void ClearLevel()
    {
        background.sprite = null;
        foreground.sprite = null;

        CharacterTurnController.Instance.ClearAll();
    }

    int GetMaxLayers(NPCCharacter[] c)
    {
        int maxLayers = 0;

        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].characterGraphics.GetComponentsInChildren<SpriteRenderer>().Length > maxLayers)
                maxLayers = c[i].characterGraphics.GetComponentsInChildren<SpriteRenderer>().Length;
        }

        return maxLayers;
    }

}
