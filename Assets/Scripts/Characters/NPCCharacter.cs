using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Character", menuName = "Characters/New NPC Character")]
public class NPCCharacter : Character
{
    [SerializeField]
    public CharacterAction[] characterActions;

    public bool isAlly;

    /*private float[] olds;

    private void OnEnable()
    {
        
    }

    private void OnValidate()
    {
        Debug.Log("!");
    }

    private float Sum(float[] o)
    {
        float s = 0;
        for(int i = 0; i < characterActions.Length; i++)
        {
            s = s + characterActions[i].chance;
        }
        return s;
    }*/
}

[Serializable]
public struct CharacterAction
{
    public enum CharacterActions
    {
        Attack,
        ShieldUp,
        Heal
    }

    public enum ActionTarget
    {
        Enemy,
        Self,
        Ally,
        NonTarget
    }

    [SerializeField]
    public CharacterActions characterAction;
    [Range(0f, 1f), SerializeField]
    public float chance;
    [SerializeField]
    public ActionTarget target;

    [SerializeField]
    public GameObject effectPrefab;
}