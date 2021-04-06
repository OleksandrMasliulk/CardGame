using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public int turnsLeft;
    public bool everyTurnTick;

    public virtual void Tick()
    {
        if (turnsLeft <= 0)
            Destroy(this);
        else
            turnsLeft--;
    }
}
