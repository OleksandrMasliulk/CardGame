using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParams : MonoBehaviour
{

    public Character character;
    public Animator animator;

    [HideInInspector]
    public int maxHealth, maxArmor, health, armor = 0;

    [HideInInspector]
    public int actionPoints, attackPower, shield, spellPower, initiative;

    [HideInInspector]
    public float dodgeChance, missChance, critChance;

    public GameObject barsPrefab;
    protected GameObject graphics;
    protected HealthBars healthBars;

    public GameObject feetAnchor, vfxPos, barsPos;

    public bool isAlly;
    public bool isPlayerCharacter = false;

    public virtual void Awake()
    {

    }

    public virtual void Init(Character c, int layersOffset)
    {
        CharacterTurnController.Instance.RegisterCharacter(this);

        //healthBars.EnableSliders();

        character = c;

        maxHealth = character.maxHealth;
        maxArmor = character.maxArmor;
        health = character.maxHealth;
        armor = 0;

        attackPower = character.defaultAttackPower;
        shield = character.defaultShield;
        spellPower = character.defaultSpellPower;

        dodgeChance = character.defaultDodgeChance;
        missChance = character.defaultMissChance;
        critChance = character.defaultCritChance;

        initiative = character.defaultInitiative;

        SetupGraphics(layersOffset);
        SetupBars();
    }

    private void SetupGraphics(int layersOffset)
    {
        graphics = Instantiate(character.characterGraphics, transform);
        //BoxCollider2D col = graphics.GetComponent<BoxCollider2D>();

        feetAnchor = graphics.transform.Find("Feet Anchor").gameObject;
        vfxPos = graphics.transform.Find("VFX Position").gameObject;
        barsPos = graphics.transform.Find("Bars Position").gameObject;

        Vector3 offset = graphics.transform.position - feetAnchor.transform.position;
        graphics.transform.localPosition += offset;

        SpriteRenderer[] sprites = graphics.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {
            Debug.Log(sprites[i].name + " before: " + sprites[i].sortingOrder);
            sprites[i].sortingOrder += layersOffset;
            Debug.Log(sprites[i].name + " after: " + sprites[i].sortingOrder);
        }

        animator = graphics.GetComponent<Animator>();
    }

    protected void SetupBars()
    {
        GameObject bars = Instantiate(barsPrefab, VFXManager.Instance.canvas.transform);
        bars.transform.position = Camera.main.WorldToScreenPoint(barsPos.transform.position);
        bars.transform.localScale = new Vector3(.7f, .7f, .7f);
        bars.transform.SetAsFirstSibling();

        healthBars = bars.GetComponentInParent<HealthBars>();

        healthBars.UpdateSliders(character.maxHealth, character.maxArmor, health, armor);
        healthBars.characterName.text = character.characterName;
    }

    private bool DodgeTest()
    {
        float rnd = Random.Range(0f, 1f);

        if (rnd <= dodgeChance)
            return true;
        else
            return false;
    }

    public virtual void TakeDamage(int damage, bool isCrit, bool canBeDodged)
    {
        if (canBeDodged & DodgeTest())
        {
            VFXManager.Instance.SpawnStringPopup(this, "DODGE");
            animator.SetTrigger("Dodge");
            return;
        }
        else
        {
            VFXManager.Instance.SpawnDamagePopup(this, damage, isCrit);

            if (armor > 0)
            {
                if (armor < damage)
                    armor = 0;
                else
                    armor -= damage;
            }
            else
                health -= damage;

            healthBars.UpdateSliders(maxHealth, maxArmor, health, armor);
            Debug.Log(gameObject.name + ": " + damage + " damage taken");

            if (health <= 0)
            {
                Die();
            }
            else
                animator.SetTrigger("Hurt");
        }
    }

    public virtual void GainArmor(int bonusArmor)
    {
        armor += bonusArmor;

        if (armor > maxArmor)
            armor = maxArmor;

        healthBars.UpdateSliders(maxHealth, maxArmor, health, armor);
        Debug.Log(gameObject.name + ": +" + bonusArmor + " armor");
    }

    public virtual void Heal(int healAmount)
    {
        if (health + healAmount > maxHealth)
            health = maxHealth;
        else
            health += healAmount;

        healthBars.UpdateSliders(maxHealth, maxArmor, health, armor);
        Debug.Log(gameObject.name + ": +" + healAmount + " hp");
    }

    public virtual void Die()
    {
        Destroy(healthBars.gameObject);
        //healthBars.DisableSliders();
        //Destroy(this.gameObject);
        animator.SetTrigger("Die");
        CharacterTurnController.Instance.CharacterDied(this);
    }

    public void KILL()
    {
        Destroy(healthBars.gameObject);
        //healthBars.DisableSliders();
        Destroy(this.gameObject);
    }
}
