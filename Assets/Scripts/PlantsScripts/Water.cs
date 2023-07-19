using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public int waterCapacity = 10;
    public int currentWaterAmount; 

    public int CurrentWaterAmount
    {
        get { return currentWaterAmount; } 
        set { currentWaterAmount = value; } 
    }

    private void Start()
    {
        CurrentWaterAmount = waterCapacity; 
    }
}
