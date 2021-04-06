using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/New Character")]
public class Character : ScriptableObject
{
    public string characterName;

    public int maxHealth;
    public int maxArmor;

    public int defaultAttackPower;
    public int defaultShield;
    public int defaultSpellPower;

    [Range(0f, 1f)]
    public float defaultMissChance;
    [Range(0f, 1f)]
    public float defaultDodgeChance;
    [Range(0f, 1f)]
    public float defaultCritChance;

    public int defaultInitiative;

    public GameObject characterGraphics;
    public Sprite characterIcon;
}



