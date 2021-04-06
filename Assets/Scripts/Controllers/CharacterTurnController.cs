using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharacterTurnController : MonoBehaviour {
    public static CharacterTurnController Instance { get; private set; }

    public List<CharacterParams> characters;

    public float timeBtwTurns;

    public Button endTurnButton;

    private void Awake() {
        Instance = this;

        characters = new List<CharacterParams>();
    }

    public void NextTurn() {
        CharacterParams c = characters[0];
        characters.Remove(characters[0]);
        characters.Add(c);

        if(!characters[0].isPlayerCharacter)
            StartCoroutine(CharacterTurn(characters[0]));
        else
            PlayerTurn();

        InitiativeUI.Instance.UpdateList();
    }

    IEnumerator CharacterTurn(CharacterParams c) {
        endTurnButton.interactable = false;
        endTurnButton.gameObject.SetActive(false);

        //yield return new WaitUntil(/*While animation runs*/);

        //yield return new WaitForSeconds(timeBtwTurns);

        CharacterAction(c.GetComponent<NPCParams>());

        float animDuration = c.animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animDuration);

        NextTurn();
    }

    public void PlayerTurn() {
        endTurnButton.interactable = true;
        endTurnButton.gameObject.SetActive(true);

        PlayerParams.Instance.RefreshAP();
        ActionPoints.Instance.UpdateField(PlayerParams.Instance.actionPoints);

        Hand.Instance.RefreshHand();
    }
    
    public void CharacterAction(NPCParams character) {
        NPCController.Instance.SetupController(character.npcChar, character);

        if (character.GetComponent<Effect>() != null)
            StartCoroutine(WaitForAllTicks(character));
        else
            NPCController.Instance.Turn();
    }

    IEnumerator WaitForAllTicks(NPCParams character)
    {
        Effect[] effects = character.GetComponents<Effect>();

        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i].everyTurnTick)
            {
                effects[i].Tick();

                float animDuration = character.animator.GetCurrentAnimatorStateInfo(0).length;
                yield return new WaitForSeconds(animDuration);
            }
            else
                continue;
        }

        NPCController.Instance.Turn();
    }

    public void RegisterCharacter(CharacterParams c) {
        characters.Add(c);
        SortList();
    }

    public void CharacterDied(CharacterParams c) {
        characters.Remove(c);
        if(!c.isAlly)
            if(EnemiesCount() == 0)
                GameController.Instance.LevelDone();

    }

    private int EnemiesCount() {
        int count = 0;

        for(int i = 0; i < characters.Count; i++)
            if(!characters[i].isAlly)
                count++;

        return count;
    }

    public void SortList() {
        characters.OrderByDescending(x => x.initiative);

    }

    public void ClearAll() {
        for(int i = 0; i < characters.Count; i++) {
            if(!characters[i].isPlayerCharacter)
                if(characters[i] != null)
                    characters[i].KILL();
        }

        for(int i = 0; i < characters.Count; i++) {
            if(!characters[i].isPlayerCharacter) {
                characters.RemoveAt(i);
            }
        }

    }
}
