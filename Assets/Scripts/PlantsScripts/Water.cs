using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public int waterCapacity = 10; // Capacidad máxima de agua en la fuente
    public int currentWaterAmount; // Cantidad actual de agua en la fuente

    public int CurrentWaterAmount
    {
        get { return currentWaterAmount; } // Getter para obtener el valor de currentWaterAmount
        set { currentWaterAmount = value; } // Setter para establecer el valor de currentWaterAmount
    }

    private void Start()
    {
        CurrentWaterAmount = waterCapacity; // Inicializar la cantidad actual de agua al máximo utilizando el setter
    }
}
