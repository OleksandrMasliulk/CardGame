using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class InitiativeElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private int mod;

    public void OnPointerEnter(PointerEventData eventData)
    {
        int index = Array.IndexOf(InitiativeUI.Instance.icons, GetComponent<Image>());
        mod = index % CharacterTurnController.Instance.characters.Count;

        VFXManager.Instance.ShowArrow(CharacterTurnController.Instance.characters[mod]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        VFXManager.Instance.HideArrow();
    }
}
