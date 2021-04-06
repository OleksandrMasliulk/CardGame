using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public static Hand Instance { get; private set; }
    private HorizontalLayoutGroup layoutGroup;

    public float cardWidth;
    public float defaultSpacing;

    public Card[] cardPool;
    public GameObject cardTemplate;
    public int maxCards = 6;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        layoutGroup = GetComponent<HorizontalLayoutGroup>();

        RefreshHand();

        RecalculateSpacing();

        DestroyHand();
    }

    public void RecalculateSpacing()
    {
        float handWidth = GetComponent<RectTransform>().rect.width;
        float cardsSumWidth = transform.childCount * cardWidth + (transform.childCount - 1) * defaultSpacing;

        //for (int i = 0; i < transform.childCount; i++) 
        //{
        //    Vector3 pos;
        //    pos.x = Mathf.Lerp(-handWidth, handWidth, (float)i / transform.childCount);
        //    pos.y = GetComponent<RectTransform>().rect.height / 2f;
        //    pos.z = 1;
        //    transform.GetChild(i).transform.position = pos;
        //}

        if (cardsSumWidth > handWidth)
        {
            float delta = handWidth - cardsSumWidth;
            layoutGroup.spacing = delta / transform.childCount;
        } 
        else
        {
            layoutGroup.spacing = defaultSpacing;
        }

        Debug.Log("Hand width: " + handWidth + " / Cards Sum Width: " + cardsSumWidth);
    }

    public void AddCard()
    {
        int index = Random.Range(0, cardPool.Length);

        GameObject card = Instantiate(cardTemplate, this.gameObject.transform);
        //card.transform.SetAsFirstSibling();

        card.GetComponentInChildren<DisplayCard>().card = cardPool[index];
    }

    public void DestroyHand()
    {
        DisplayCard[] cardsInHand = GetComponentsInChildren<DisplayCard>();
        foreach (DisplayCard card in cardsInHand)
        {
            Destroy(card.transform.parent.gameObject);
        }
    }

    public void RefreshHand()
    {
        DestroyHand();
        for (int i = 0; i < maxCards; i++)
            AddCard();
    } 
}
