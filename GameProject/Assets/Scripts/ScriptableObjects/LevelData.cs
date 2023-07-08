using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    public List<Placeable> placeables;
}
