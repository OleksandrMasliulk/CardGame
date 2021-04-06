using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCard : MonoBehaviour {
    //private int siblingIndex;

    public Card card;

    public Text nameText;
    public Text descriptionText;

    public Image artImage;

    public Text apCostText;

    public Button buttonComponent;

    private void Awake() {
    }

    void Start() {
        //    RecalculateSiblingIndex();
        buttonComponent.onClick.AddListener(() => CardUseTest.Instance.PickCard(card, transform.parent.gameObject));

        nameText.text = card.cardName;
        descriptionText.text = card.cardDescription.Replace("N", ValueToReplace().ToString());

        artImage.sprite = card.cardArt;

        apCostText.text = card.apCost.ToString();
    }

    int ValueToReplace()
    {
        int n = 0;

        if (card.spell == Card.Spell.chain_lightning)
        {
            ChainLightningCardController cc = new ChainLightningCardController(card, this.gameObject);
            n = (int)(cc.spModifier * PlayerParams.Instance.spellPower);
        }
        else if (card.spell == Card.Spell.fireball)
        {
            FireballCardController cc = new FireballCardController(card, this.gameObject);
            n = (int)(cc.spModifier * PlayerParams.Instance.spellPower);
        }
        else if (card.spell == Card.Spell.slash)
        {
            SlashCardController cc = new SlashCardController(card, this.gameObject);
            n = (int)(cc.apModifier * PlayerParams.Instance.attackPower);
        }
        else if (card.spell == Card.Spell.shield_block)
        {
            ShieldBlockCardController cc = new ShieldBlockCardController(card, this.gameObject);
            n = (int)(cc.shieldModifier * PlayerParams.Instance.shield);
        }
        else if (card.spell == Card.Spell.throw_rock)
        {
            ThrowCardController cc = new ThrowCardController(card, this.gameObject);
            n = (int)(cc.apModifier * PlayerParams.Instance.attackPower);
        }

        return n;
    }
    //private void RecalculateSiblingIndex()
    //{
    //    siblingIndex = transform.GetSiblingIndex();
    //}

    //public void ToFront()
    //{
    //    transform.SetAsLastSibling();
    //}

    //public void ToStartPlace()
    //{
    //    transform.SetSiblingIndex(siblingIndex);
    //}
}
