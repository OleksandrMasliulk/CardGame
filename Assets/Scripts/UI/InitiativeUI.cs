using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeUI : MonoBehaviour
{
    public static InitiativeUI Instance { get; private set; }

    public Image[] icons;

    private void Awake()
    {
        Instance = this;
    }


    public void UpdateList()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            int mod = i % CharacterTurnController.Instance.characters.Count;

            icons[i].sprite = CharacterTurnController.Instance.characters[mod].character.characterIcon;
            if (CharacterTurnController.Instance.characters[mod].isPlayerCharacter)
            {
                icons[i].transform.parent.GetComponent<Image>().color = Color.blue;
            }
            else if (CharacterTurnController.Instance.characters[mod].isAlly)
            {
                icons[i].transform.parent.GetComponent<Image>().color = new Color(130f / 255f, 188f / 255f, 139f / 255f);
            }
            else
            {
                icons[i].transform.parent.GetComponent<Image>().color = new Color(200f / 255f, 105f / 255f, 105f / 255f);
            }
        }

        icons[0].transform.parent.GetComponent<Image>().color = new Color(0f / 255f, 126f / 255f, 20f / 255f);
    }
}
