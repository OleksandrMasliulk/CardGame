using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateNPCWindow : EditorWindow
{
    enum CharacterType
    {
        Ally,
        Enemy,
        Boss
    }  

    NPCCharacter character;
    NPCCharacter characterToCompare;
    CharacterType type;

    Vector2 scrollPos;

    [MenuItem("Window/Create Character/New NPC")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CreateNPCWindow));
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

        type = (CharacterType)EditorGUILayout.EnumPopup("Character Type", type);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Character To Compare");
        characterToCompare = (NPCCharacter)EditorGUILayout.ObjectField(characterToCompare, typeof(NPCCharacter), false);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();

        DrawFields();
        DrawEditableColumn(character);

        if (characterToCompare != null)
            DrawDisplayColumn(characterToCompare);

        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("Save"))
        {
            SaveCharacter();
        }
        EditorGUILayout.EndScrollView();
    }

    private void OnEnable()
    {
        character = ScriptableObject.CreateInstance<NPCCharacter>();
        character.characterName = "New Character";
    }

    void SaveCharacter()
    {
        string path;

        if (type == CharacterType.Ally)
            path = "Assets/Characters/Allies/";//AssetDatabase.CreateAsset(character, "Assets/Characters/Allies/" + character.characterName + ".asset");
        else if (type == CharacterType.Enemy)
            path = "Assets/Characters/Enemies/";// AssetDatabase.CreateAsset(character, "Assets/Characters/Enemies/" + character.characterName + ".asset");
        else
            path = "Assets/Characters/Bosses/";// AssetDatabase.CreateAsset(character, "Assets/Characters/Bosses/" + character.characterName + ".asset");

        if (!AssetDatabase.IsValidFolder(path))
            Directory.CreateDirectory(path);
   
        AssetDatabase.CreateAsset(character, path + character.characterName + ".asset");
   
        ResetAsset();
    }

    void ResetAsset()
    {
        character = null;
        character = ScriptableObject.CreateInstance<NPCCharacter>();
        character.characterName = "New Character";
    }

    void DrawFields()
    {
        EditorGUILayout.BeginVertical();

        GUILayout.Label("Name");
        GUILayout.Label("Max Health");
        GUILayout.Label("Max Armor");
        GUILayout.Label("Initiative");
        GUILayout.Label("Default Attack Power");
        GUILayout.Label("Default Shield");
        GUILayout.Label("Default Spell Power");
        GUILayout.Label("Crit Chance");
        GUILayout.Label("Miss Chance");
        GUILayout.Label("Dodge Chance");
        GUILayout.Label("Character Graphics");
        GUILayout.Label("Chatacter Icon");
        GUILayout.Label("Actions");

        EditorGUILayout.EndVertical();
    }

    void DrawEditableColumn(NPCCharacter _character)
    {
        EditorGUILayout.BeginVertical();

        if (type == CharacterType.Ally)
            character.isAlly = true;
        else if (type == CharacterType.Enemy)
            character.isAlly = false;
        else
            character.isAlly = false;

        _character.characterName = EditorGUILayout.TextField(_character.characterName);
        _character.maxHealth = EditorGUILayout.IntField(_character.maxHealth);
        _character.maxArmor = EditorGUILayout.IntField(_character.maxArmor);
        _character.defaultInitiative = EditorGUILayout.IntField(_character.defaultInitiative);

        _character.defaultAttackPower = EditorGUILayout.IntField(_character.defaultAttackPower);
        _character.defaultShield = EditorGUILayout.IntField(_character.defaultShield);
        _character.defaultSpellPower = EditorGUILayout.IntField(_character.defaultSpellPower);

        _character.defaultCritChance = EditorGUILayout.Slider(_character.defaultCritChance, 0f, 1f);
        _character.defaultMissChance = EditorGUILayout.Slider(_character.defaultMissChance, 0f, 1f);
        _character.defaultDodgeChance = EditorGUILayout.Slider(_character.defaultDodgeChance, 0f, 1f);

        _character.characterGraphics = (GameObject)EditorGUILayout.ObjectField(_character.characterGraphics, typeof(GameObject), false);
        _character.characterIcon = (Sprite)EditorGUILayout.ObjectField(_character.characterIcon, typeof(Sprite), false);

        SerializedObject so = new SerializedObject(_character);
        SerializedProperty sp = so.FindProperty("characterActions");
        EditorGUILayout.PropertyField(sp);
        so.ApplyModifiedProperties();

        EditorGUILayout.EndVertical();
    }

    void DrawDisplayColumn(NPCCharacter _character)
    {
        EditorGUILayout.BeginVertical();

        GUILayout.Label(_character.characterName);
        GUILayout.Label(_character.maxHealth.ToString());
        GUILayout.Label(_character.maxArmor.ToString());
        GUILayout.Label(_character.defaultInitiative.ToString());
        GUILayout.Label(_character.defaultAttackPower.ToString());
        GUILayout.Label(_character.defaultShield.ToString());
        GUILayout.Label(_character.defaultSpellPower.ToString());
        GUILayout.Label(_character.defaultCritChance.ToString());
        GUILayout.Label(_character.defaultMissChance.ToString());
        GUILayout.Label(_character.defaultDodgeChance.ToString());
        if (_character.characterGraphics != null)
            GUILayout.Label(_character.characterGraphics.name);
        if (character.characterIcon != null)
            GUILayout.Label(_character.characterIcon.name);

        if (_character.characterActions != null)
            foreach(CharacterAction a in _character.characterActions)
            {
                GUILayout.Space(40);
                GUILayout.Label(a.characterAction.ToString());
                GUILayout.Label(a.chance.ToString());
                GUILayout.Label(a.target.ToString());
                if (a.effectPrefab != null)
                    GUILayout.Label(a.effectPrefab.name);
            }
       
        EditorGUILayout.EndVertical();
    }

    /*
     type = (CharacterType)EditorGUILayout.EnumPopup("Character Type", type);
            if (type == CharacterType.Ally)
                character.isAlly = true;
            else if (type == CharacterType.Enemy)
                character.isAlly = false;
            else
                character.isAlly = false;

            GUILayout.Label("Parameters", EditorStyles.boldLabel);
            character.characterName = EditorGUILayout.TextField("Name", character.characterName);
            character.maxHealth = EditorGUILayout.IntField("Max Health", character.maxHealth);
            character.maxArmor = EditorGUILayout.IntField("Max Armor", character.maxArmor);
            character.defaultInitiative = EditorGUILayout.IntField("Initiative", character.defaultInitiative);

            character.defaultAttackPower = EditorGUILayout.IntField("Default Attack Power", character.defaultAttackPower);
            character.defaultShield = EditorGUILayout.IntField("Default Shield", character.defaultShield);
            character.defaultSpellPower = EditorGUILayout.IntField("Default Spell Power", character.defaultSpellPower);

            character.defaultCritChance = EditorGUILayout.Slider("Crit Chance", character.defaultCritChance, 0f, 1f);
            character.defaultMissChance = EditorGUILayout.Slider("Miss Chance", character.defaultMissChance, 0f, 1f);
            character.defaultDodgeChance = EditorGUILayout.Slider("Dodge Chance", character.defaultDodgeChance, 0f, 1f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Character Graphics");
            character.characterGraphics = (GameObject)EditorGUILayout.ObjectField(character.characterGraphics, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Actions", EditorStyles.boldLabel);
            SerializedObject so = new SerializedObject(character);
            SerializedProperty sp = so.FindProperty("characterActions");
            EditorGUILayout.PropertyField(sp);
            so.ApplyModifiedProperties();
     */
}