using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantConfig", menuName = "PlantConfig")]
public class PlantConfig : ScriptableObject
{
    public string plantName;
    public Sprite[] growthSprites;
    public float growthTime;
    public float wateringTime;
    public int quantityOfWaterNeeded;
}
