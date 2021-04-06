using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance { get; private set; }

    public GameObject damagePopup;
    public GameObject critDamagePopup;
    public GameObject arrow;
    public Canvas canvas;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnEffect(GameObject VFX, CharacterParams character)
    {
        GameObject e = Instantiate(VFX, character.vfxPos.transform.position, Quaternion.identity);
        if (!character.isAlly)
        {
            Vector3 scale = e.transform.localScale;
            scale.x *= -1;
            e.transform.localScale = scale;
        }

        Animator a = e.GetComponent<Animator>();
        float animDuration = a.GetCurrentAnimatorStateInfo(0).length;

        Destroy(e, animDuration);
    }

    public void SpawnDamagePopup(CharacterParams character, int damage, bool isCrit)
    {
        GameObject p;

        if (isCrit)
            p = Instantiate(critDamagePopup, canvas.transform);
        else
            p = Instantiate(damagePopup, canvas.transform);

        p.transform.position = Camera.main.WorldToScreenPoint(character.barsPos.transform.position);
        //p.transform.localScale *= p.transform.parent.localScale.x;
        p.GetComponent<Text>().text = "-" + damage.ToString();

        Animator a = p.GetComponent<Animator>();
        float animDuration = a.GetCurrentAnimatorStateInfo(0).length;

        Destroy(p, animDuration);
    }

    public void SpawnStringPopup(CharacterParams character, string message)
    {
        GameObject m = Instantiate(damagePopup, canvas.transform);

        m.transform.position = Camera.main.WorldToScreenPoint(character.barsPos.transform.position);
        m.GetComponent<Text>().text = message;

        Animator a = m.GetComponent<Animator>();
        float animDuration = a.GetCurrentAnimatorStateInfo(0).length;

        Destroy(m, animDuration);
    }
    
    public void ShowArrow(CharacterParams _character)
    {
        arrow.SetActive(true);
        arrow.transform.position = Camera.main.WorldToScreenPoint(_character.barsPos.transform.position + new Vector3(0, 1.1f, 0));
    }

    public void HideArrow()
    {
        arrow.SetActive(false);
    }
}
