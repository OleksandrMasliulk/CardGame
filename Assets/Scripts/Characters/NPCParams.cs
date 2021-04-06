using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCParams : CharacterParams
{
    [HideInInspector]
    public NPCCharacter npcChar;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Init(Character c, int layersOffset)
    {
        npcChar = c as NPCCharacter;

        base.Init(c, layersOffset);

        isAlly = npcChar.isAlly;
    }
}
