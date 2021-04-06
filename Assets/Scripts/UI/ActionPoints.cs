using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPoints : MonoBehaviour {
    //singleton
    public static ActionPoints Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
    public void UpdateField(int newValue) {
        gameObject.GetComponent<Text>().text = "AP: " + newValue.ToString();
    }
}
