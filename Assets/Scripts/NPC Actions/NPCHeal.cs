using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHeal : NPCActionController
{
    public NPCHeal(CharacterParams _user, GameObject _VFX) : base(_user, _VFX)
    {
    }

    public override void Action(CharacterParams target)
    {
        base.Action(target);

        user.animator.SetTrigger("Cast");
        if (VFX != null)
            VFXManager.Instance.SpawnEffect(VFX, target);

        target.Heal(user.spellPower);
    }
}
