using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/New Card")]
public class Card : ScriptableObject {

    public enum CardType {
        attack,
        defence,
        special
    }

    public enum Spell {
        fireball,
        slash,
        shield_block,
        concentration,
        throw_rock,
        chain_lightning

    }

    public CardType cardType;
    public Spell spell;

    public string cardName;
    public string cardDescription;

    public Sprite cardArt;

    public bool isTargeted = true;
    public int apCost;

    public GameObject effectPrefab;
}
