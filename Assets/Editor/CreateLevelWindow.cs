using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.IO;

public class CreateLevelWindow : EditorWindow
{
    enum LevelZone
    {
        Forest,
        Ruins,
        Foothills
    }

    Vector2 scrollPos;

    LevelZone zone;
    Level level;
    int alliesCount;
    int enemiesCount;

    Scene newScene;
    public GameObject uiCanvas;
    public GameObject graphicsCanvas;
    public GameObject eventSystem;

    [MenuItem("Window/Create Level")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CreateLevelWindow));
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

        zone = (LevelZone)EditorGUILayout.EnumPopup("Zone", zone);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Background Image");
        level.backgroundImage = (Sprite)EditorGUILayout.ObjectField(level.backgroundImage, typeof(Sprite), false);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Foreground Image");
        level.foregroundImage = (Sprite)EditorGUILayout.ObjectField(level.foregroundImage, typeof(Sprite), false);
        EditorGUILayout.EndHorizontal();

        alliesCount = EditorGUILayout.IntSlider("Allies Count", alliesCount, 0, 2);
        if (level.allies == null || level.allies.Length != alliesCount)
            level.allies = new NPCCharacter[alliesCount];
        for(int i = 0; i < alliesCount; i++)
            level.allies[i] = (NPCCharacter)EditorGUILayout.ObjectField(level.allies[i], typeof(NPCCharacter), false);

        enemiesCount = EditorGUILayout.IntSlider("Enemies Count", enemiesCount, 0, 3);
        if(level.enemies == null || level.enemies.Length != enemiesCount)
            level.enemies = new NPCCharacter[enemiesCount];
        for (int i = 0; i < enemiesCount; i++)
            level.enemies[i] = (NPCCharacter)EditorGUILayout.ObjectField(level.enemies[i], typeof(NPCCharacter), false);

        level.index = EditorGUILayout.IntField("Level Index", level.index);

        if (GUILayout.Button("Save"))
        {
            SaveLevel();

            CreateScene();
            SaveScene();

            ResetAsset();
        }

        EditorGUILayout.EndScrollView();
    }

    private void OnEnable()
    {
        level = ScriptableObject.CreateInstance<Level>();
    }

    void ResetAsset()
    {
        level = null;
        level = ScriptableObject.CreateInstance<Level>();
    } 

    void SaveLevel()
    {
        string path = "Assets/Levels/" + zone.ToString() + "/";

        if (!AssetDatabase.IsValidFolder(path))
            Directory.CreateDirectory(path);

        if (!AssetDatabase.Contains(level))
            AssetDatabase.CreateAsset(level, path + zone.ToString() + " " + level.index + ".asset");
        else
        {
            Level assetToOverwrite = AssetDatabase.LoadAssetAtPath<Level>(path + zone.ToString() + " " + level.index + ".asset");
            assetToOverwrite = level;
            AssetDatabase.SaveAssets();
        }
            
    } 

    void CreateScene()
    {
        Scene currentScene = EditorSceneManager.GetActiveScene();
        newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
        EditorSceneManager.SetActiveScene(newScene);
        Instantiate(eventSystem);
        GameObject ui = Instantiate(uiCanvas);
        GameObject graphics = Instantiate(graphicsCanvas);
        graphics.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();

        LevelLoader loader = ui.transform.Find("Controllers").GetComponentInChildren<LevelLoader>(true);
        loader.level = level;
        loader.background = graphics.transform.Find("Background").GetComponent<Image>();
        loader.foreground = graphics.transform.Find("Foreground").GetComponent<Image>();

        loader.playerSlot = graphics.transform.Find("Foreground").Find("Players").Find("Player Slot 1").gameObject;

        loader.allySlots[0] = graphics.transform.Find("Foreground").Find("Players").Find("Player Slot 3").gameObject;
        loader.allySlots[1] = graphics.transform.Find("Foreground").Find("Players").Find("Player Slot 2").gameObject;

        loader.enemySlots[0] = graphics.transform.Find("Foreground").Find("Enemies").Find("Enemy slot 1").gameObject;
        loader.enemySlots[1] = graphics.transform.Find("Foreground").Find("Enemies").Find("Enemy slot 2").gameObject;
        loader.enemySlots[2] = graphics.transform.Find("Foreground").Find("Enemies").Find("Enemy slot 3").gameObject;

        EditorSceneManager.SetActiveScene(currentScene);
    }

    void SaveScene()
    {
        string path = "Assets/Scenes/" + zone.ToString() + "/";

        EditorSceneManager.SaveScene(newScene, path + zone.ToString() + " " + level.index + ".unity");

        EditorSceneManager.UnloadSceneAsync(newScene);
    }
}