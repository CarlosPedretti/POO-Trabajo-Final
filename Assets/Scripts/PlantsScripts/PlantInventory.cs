using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlantInventoryManager
{
    private static Dictionary<string, int> plantInventory = new Dictionary<string, int>();

    public static Dictionary<string, int> PlantInventory
    {
        get { return plantInventory; }
    }

    public static void CollectPlant(string plantName, int amount)
    {
        if (plantInventory.ContainsKey(plantName))
        {
            plantInventory[plantName] += amount;
        }
        else
        {
            plantInventory[plantName] = amount;
        }

        GameManager.Instance.UpdateSeedUI(plantName, GetPlantCount(plantName));
    }

    public static int GetPlantCount(string plantName)
    {
        if (plantInventory.ContainsKey(plantName))
        {
            return plantInventory[plantName];
        }
        else
        {
            return 0;
        }
    }

    public static int GetTotalPlantsCollected()
    {
        int totalPlants = 0;
        foreach (var kpv in plantInventory)
        {
            totalPlants += kpv.Value;
        }
        return totalPlants;
    }
}
