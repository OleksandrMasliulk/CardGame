using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCActionController
{
    public CharacterParams user;
    public GameObject VFX;

    public NPCActionController(CharacterParams _user, GameObject _VFX)
    {
        user = _user;
        VFX = _VFX;
    }

    public virtual void Action(CharacterParams target)
    {
    }

    protected bool AttackTest()
    {
        float rnd = Random.Range(0f, 1f);

        if (rnd >= user.missChance)
            return true;
        else return false;
    }

    protected bool CritTest()
    {
        float rnd = Random.Range(0f, 1f);

        if (rnd <= user.critChance)
            return true;
        else return false;
    }

}
