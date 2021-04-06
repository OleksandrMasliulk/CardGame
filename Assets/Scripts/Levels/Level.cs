using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "Levels/New Level")]
public class Level : ScriptableObject
{
    public Sprite backgroundImage;
    public Sprite foregroundImage;

    public NPCCharacter[] enemies;
    public NPCCharacter[] allies;

    public int index;
}


