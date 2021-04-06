using UnityEngine.UI;
using UnityEngine;

public class PlayerParams : CharacterParams {
    public static PlayerParams Instance { get; private set; }

    [HideInInspector]
    public PlayerCharacter playerChar;

    public override void Awake() {
        if (Instance == null)
        {
            base.Awake();
            Instance = this;
            //base.Init(character);

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(healthBars.gameObject);
            Destroy(this.gameObject);
        }
    }

    public override void Init(Character c, int layersOffset)
    {
        Debug.LogWarning("!!! PLAYER INIT !!!" + name);

        playerChar = c as PlayerCharacter;

        base.Init(c, layersOffset);

        character = playerChar;
        isPlayerCharacter = true;
        isAlly = true;

    }

    public override void Die() {
        base.Die();

        GameController.Instance.Lost();
    }

    public void RefreshPlayer() {
        CharacterTurnController.Instance.RegisterCharacter(this);

        health = maxHealth;
        armor = 0;
        actionPoints = 3;

        healthBars.EnableSliders();

        healthBars.UpdateSliders(maxHealth, maxArmor, health, armor);
        healthBars.characterName.text = gameObject.name;
    }

    public void RefreshAP() {
        actionPoints = 3;
    }

    public void NewLevelInit()
    {
        CharacterTurnController.Instance.RegisterCharacter(this);

        SetupBars();
    }
}
