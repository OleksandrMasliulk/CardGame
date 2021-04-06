using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterAction;

public class NPCController : MonoBehaviour {
    public static NPCController Instance { get; private set; }

    public NPCCharacter character;
    NPCActionController[] actions;

    public NPCParams characterParams;

    private void Awake() {
        Instance = this;
    }

    public void SetupController(NPCCharacter _character, NPCParams _characterParams) {
        character = _character;
        characterParams = _characterParams;

        actions = new NPCActionController[character.characterActions.Length];

        for(int i = 0; i < character.characterActions.Length; i++) {
            if(character.characterActions[i].characterAction == CharacterActions.Attack)
                actions[i] = new NPCAttack(characterParams, character.characterActions[i].effectPrefab);
            if(character.characterActions[i].characterAction == CharacterActions.ShieldUp)
                actions[i] = new NPCShieldUp(characterParams, character.characterActions[i].effectPrefab);
            if(character.characterActions[i].characterAction == CharacterActions.Heal)
                actions[i] = new NPCHeal(characterParams, character.characterActions[i].effectPrefab);
        }
    }

    public void Turn() {
        float rnd = Random.Range(0f, 1f);

        Debug.Log(rnd);

        CharacterAction rndAction = ActionChance(character.characterActions, rnd);

        CharacterParams target = ChooseTarget(rndAction);

        for(int i = 0; i < character.characterActions.Length; i++) {
            if(rndAction.characterAction == character.characterActions[i].characterAction)
                if(target != null || rndAction.target == ActionTarget.NonTarget)
                    actions[i].Action(target);
        }
    }

    public CharacterAction ActionChance(CharacterAction[] action, float rndChance) {
        int i;
        float sum = action[0].chance;
        for(i = 1; i < action.Length; i++) {
            if(sum >= rndChance)
                return action[i - 1];
            else
                sum = sum + action[i].chance;
        }
        return action[i - 1];
    }

    public CharacterParams ChooseTarget(CharacterAction action) {
        CharacterParams target = null;

        List<CharacterParams> allies = new List<CharacterParams>();
        List<CharacterParams> enemies = new List<CharacterParams>();

        foreach(CharacterParams t in CharacterTurnController.Instance.characters) {
            if(characterParams.isAlly) {
                if(!t.isAlly)
                    enemies.Add(t);
                else
                    allies.Add(t);
            } else {
                if(t.isAlly)
                    enemies.Add(t);
                else
                    allies.Add(t);
            }
        }
        //for (int i = 0; i < enemies.Length; i++)
        //    Debug.Log("Enemy " + (i + 1) + ": " + enemies);
        //for (int i = 0; i < allies.Length; i++)
        //    Debug.Log("Ally " + (i + 1) + ": " + allies);

        if(action.target == ActionTarget.Ally) {
            int rand = Random.Range(0, allies.Count);
            target = allies[rand];
        }
        if(action.target == ActionTarget.Enemy) {
            int rand = Random.Range(0, enemies.Count);
            if(enemies.Count != 0)
                target = enemies[rand];
            else
                ;
        }
        if(action.target == ActionTarget.Self) {
            target = characterParams;
        }
        if(action.target == ActionTarget.NonTarget) {
            return null;
        }

        return target;
    }
}
