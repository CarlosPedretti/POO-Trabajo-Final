using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptorPlanta : MonoBehaviour
{

    public PlantConfig plantConfig;

    private Planta plantedPlant;


    void Start()
    {

    }


    public bool IsPlantable()
    {
        bool isPlantable = plantedPlant == null;
        Debug.Log("La planta es plantable: " + isPlantable);
        return isPlantable;
    }

    public void Plant()
    {
        if (!IsPlantable())
        {
            Debug.Log("Ya hay una planta plantada en este receptor");
            return;
        }


        Debug.Log("Plant() called");
        GameObject newPlantObject = new GameObject("Plant_" + plantConfig.plantName);
        newPlantObject.transform.position = transform.position;
        newPlantObject.transform.SetParent(transform);
        plantedPlant = newPlantObject.AddComponent<Planta>();
        plantedPlant.Setup(plantConfig);
    }

    public void Harvest()
    {
        if (plantedPlant == null)
        {
            Debug.Log("No hay una planta plantada en este receptor");
            return;
        }

        plantedPlant.Harvesting();
        plantedPlant = null;
    }
}
