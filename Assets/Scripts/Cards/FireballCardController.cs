using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCardController : CardController
{
    public float spModifier = 1.5f;

    public int damage;
    public int debuffDuration = 3;
    public int burnDamage;

    public FireballCardController(Card d, GameObject g) : base(d, g)
    {
    }

    public override void Use(CharacterParams target)
    {
        base.Use(target);

        PlayerParams.Instance.animator.SetTrigger("Cast");

        damage = (int)(PlayerParams.Instance.spellPower * spModifier);
        burnDamage = damage / 2;

        if (data.effectPrefab != null)
            VFXManager.Instance.SpawnEffect(data.effectPrefab, target);
        target.TakeDamage(damage, false, true);

        //if (target.GetComponent<BurnDebuff>() == null)
        //{
        //    BurnDebuff debuff = target.gameObject.AddComponent<BurnDebuff>();
        //   debuff.Setup(debuffDuration, burnDamage);
        //}
        //else
        //    target.GetComponent<BurnDebuff>().turnsLeft = debuffDuration;
    }
}
