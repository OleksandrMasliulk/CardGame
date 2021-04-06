using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningCardController : CardController
{
    public float spModifier = 1f;

    public int primaryTargetDamage;
    public int secondaryTargetDamage;

    public ChainLightningCardController(Card d, GameObject g) : base(d, g)
    {
    }
    public override void Use(CharacterParams target)
    {
        base.Use(target);

        PlayerParams.Instance.animator.SetTrigger("Cast");

        primaryTargetDamage = (int)(PlayerParams.Instance.spellPower * spModifier);
        secondaryTargetDamage = (int)((PlayerParams.Instance.spellPower * spModifier) / 2);

        CharacterParams[] targets = CharacterTurnController.Instance.characters.ToArray();
        
        foreach (CharacterParams character in targets)
        {
            if (!character.isAlly)
            {
                if (data.effectPrefab != null)
                    VFXManager.Instance.SpawnEffect(data.effectPrefab, character);
                if (character == target)
                    character.TakeDamage(primaryTargetDamage, false, false);
                else
                    character.TakeDamage(secondaryTargetDamage, false, false);
            }
        }
    }

}
