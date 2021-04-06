using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashCardController : CardController
{
    public float apModifier = 1;

    public int damage;

    public SlashCardController(Card d, GameObject g) : base(d, g)
    {
    }

    public override void Use(CharacterParams target)
    {
        base.Use(target);

        PlayerParams.Instance.animator.SetTrigger("Attack");

        damage = (int)(PlayerParams.Instance.attackPower * apModifier);
        if (data.effectPrefab != null)
            VFXManager.Instance.SpawnEffect(data.effectPrefab, target);

        target.TakeDamage(damage, false, true);
    }
}
