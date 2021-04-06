using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadSystem
{
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerScore.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Stats saved");
    }

    public static void Load()
    {
        string path = Application.persistentDataPath + "/playerScore.bin";
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            PlayerProgress.isForestFinished = data.forest;
            PlayerProgress.isRuinsFinished = data.ruins;
            PlayerProgress.isFoothillsFinished = data.foothills;
        }
        else
        {
            PlayerProgress.isForestFinished = false;
            PlayerProgress.isRuinsFinished = false;
            PlayerProgress.isFoothillsFinished = false;

            Save();
            return;
        }

    }
}

[System.Serializable]
public class PlayerData
{
    public bool forest;
    public bool ruins;
    public bool foothills;

    public PlayerData()
    {
        forest = PlayerProgress.isForestFinished;
        ruins = PlayerProgress.isRuinsFinished;
        foothills = PlayerProgress.isFoothillsFinished;
    }
}
