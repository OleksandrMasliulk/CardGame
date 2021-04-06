using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTroll : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("Troll Was Defeated!");
        //PlayerProgress.isForestFinished = true;
        SaveLoadSystem.Save();
    }
}
