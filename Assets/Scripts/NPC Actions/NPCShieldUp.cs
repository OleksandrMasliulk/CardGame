using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShieldUp : NPCActionController
{
    public NPCShieldUp(CharacterParams _user, GameObject _VFX) : base(_user, _VFX)
    {
    }

    public override void Action(CharacterParams target)
    {
        base.Action(target);

        user.animator.SetTrigger("Action");
        if (VFX != null)
            VFXManager.Instance.SpawnEffect(VFX, target);

        user.GainArmor(user.shield);
    }
}
