using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    //public GameObject parameters;
    //public Vector3 positionOffset;

    public Text characterName;
    public Slider healthSlider;
    public Slider armorSlider;

    // Start is called before the first frame update
    private void Awake()
    {
        //SetPosition();
    }

    public void UpdateSliders(int newMaxHealthValue, int newMaxArmorValue, int newHealthValue, int newArmorValues)
    {
        healthSlider.maxValue = newMaxHealthValue;
        armorSlider.maxValue = newMaxArmorValue;

        healthSlider.value = newHealthValue;
        armorSlider.value = newArmorValues;
    }

    //void SetPosition()
    //{
    //    parameters.transform.position = transform.position + positionOffset;
    //}

    public void EnableSliders()
    {
        //parameters.SetActive(true);
        Debug.Log(gameObject.name + ": Sliders enabled");
    }

    public void DisableSliders()
    {
        //parameters.SetActive(false);
        Debug.Log(gameObject.name + ": Sliders disabled");
    }

}
