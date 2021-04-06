using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack : NPCActionController
{
    public NPCAttack(CharacterParams _user, GameObject _VFX) : base(_user, _VFX)
    {
    }

    public override void Action(CharacterParams target)
    {
        base.Action(target);

        user.animator.SetTrigger("Attack");
        if (VFX != null)
            VFXManager.Instance.SpawnEffect(VFX, target);

        if (AttackTest())
        {
            if (CritTest())
            {
                target.TakeDamage(user.attackPower * 2, true, true);
                Debug.LogWarning(user.name + ": CRIT");
            }
            else
                target.TakeDamage(user.attackPower, false, true);
        }
        else
            VFXManager.Instance.SpawnStringPopup(target, "MISS");
    }
}
