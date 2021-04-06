using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDebuff : Effect
{
    private int damage;
    private CharacterParams character;

    public void Setup(int _turnsLeft, int _damage)
    {
        turnsLeft = _turnsLeft;
        damage = _damage;
        everyTurnTick = true;

        character = GetComponent<CharacterParams>();
    }

    public override void Tick()
    {
        base.Tick();

        Effect();
    }

    void Effect()
    {
        //VFXManager.Instance.SpawnEffect(null, character);
        character.TakeDamage(damage, false, false);
    }
}
