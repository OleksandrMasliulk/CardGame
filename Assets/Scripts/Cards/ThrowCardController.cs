using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCardController : CardController
{
    public float apModifier = 0.5f;

    public int damage;

    public ThrowCardController(Card d, GameObject g) : base(d, g)
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
