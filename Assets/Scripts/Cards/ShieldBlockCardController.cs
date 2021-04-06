using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlockCardController : CardController
{
    public float shieldModifier = 1;

    public int bonusArmor;

    public ShieldBlockCardController(Card d, GameObject g) : base(d, g)
    {
    }

    public override void Use(CharacterParams target)
    {
        base.Use(target);

        PlayerParams.Instance.animator.SetTrigger("Action");

        bonusArmor = (int)(PlayerParams.Instance.shield * shieldModifier);
        if (data.effectPrefab != null)
            VFXManager.Instance.SpawnEffect(data.effectPrefab, target);

        target.GainArmor(bonusArmor);  
    }
}
