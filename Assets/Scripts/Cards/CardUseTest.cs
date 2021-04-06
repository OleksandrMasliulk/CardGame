using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUseTest : MonoBehaviour {

    public static CardUseTest Instance { get; private set; }

    public GameObject cardGO;
    CardController selectedCard;

    public bool isChosing;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if(isChosing) {
            if(Input.GetMouseButton(0)) {
                CharacterChosen();
            }
        }
    }

    public void PickCard(Card card, GameObject go)
    {
        if (CharacterTurnController.Instance.characters[0].isPlayerCharacter)
        {
            if (selectedCard != null)
            {
                //Odznaczenie innych kart - trzeba dodać
                // selectedCard.UnPick();
                selectedCard = null;
                cardGO = null;
            }

            if (PlayerParams.Instance.actionPoints >= card.apCost)
            {
                cardGO = go;
                if (card.spell == Card.Spell.fireball)
                    selectedCard = new FireballCardController(card, cardGO);
                if (card.spell == Card.Spell.shield_block)
                    selectedCard = new ShieldBlockCardController(card, cardGO);
                if (card.spell == Card.Spell.slash)
                    selectedCard = new SlashCardController(card, cardGO);
                if (card.spell == Card.Spell.throw_rock)
                    selectedCard = new ThrowCardController(card, cardGO);
                if (card.spell == Card.Spell.chain_lightning)
                    selectedCard = new ChainLightningCardController(card, cardGO);

                if (selectedCard.data.isTargeted)
                    StartChose();

                Debug.Log("Card Chosen: " + selectedCard.data.cardName);
            }
            else
            {
                Debug.Log("Not enought AP!");
            }
        }
    }

    void StartChose() {
        isChosing = true;
    }

    public void CharacterChosen() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);

        if(selectedCard.data.cardType == Card.CardType.attack) {
            if(hit.collider != null && !hit.collider.GetComponentInParent<CharacterParams>().isAlly) {
                CharacterParams target = hit.collider.GetComponentInParent<CharacterParams>();
                selectedCard.Use(target);
                PlayerParams.Instance.actionPoints -= selectedCard.data.apCost;
                FindObjectOfType<ActionPoints>().UpdateField(PlayerParams.Instance.actionPoints);
                Destroy(cardGO);
            }
        }

        if(selectedCard.data.cardType == Card.CardType.defence) {
            if(hit.collider != null && hit.collider.GetComponentInParent<CharacterParams>().isAlly) {
                CharacterParams target = hit.collider.GetComponentInParent<CharacterParams>();
                selectedCard.Use(target);
                PlayerParams.Instance.actionPoints -= selectedCard.data.apCost;
                FindObjectOfType<ActionPoints>().UpdateField(PlayerParams.Instance.actionPoints);
                Destroy(cardGO);
            }
        }

        selectedCard = null;
        isChosing = false;
        //CharacterTurnController.Instance.NextTurn();
    }
}
