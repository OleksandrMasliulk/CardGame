using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverCharacter : MonoBehaviour
{
    private CharacterParams p;

    private void Start()
    {
        p = GetComponentInParent<CharacterParams>();
    }

    private void OnMouseEnter()
    {
        if (CardUseTest.Instance.isChosing)
            VFXManager.Instance.ShowArrow(p);
    }

    private void OnMouseExit()
    {
        VFXManager.Instance.HideArrow(); 
    }
}
