using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController {

    public Card data;
    public GameObject go;

    public virtual void Use(CharacterParams target)
    {
    }

    public CardController(Card d, GameObject g) {
        data = d;
        go = g;
    }

}
