using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject growthSprite;
    public float wateringTime;
    public float growthTime;

    private bool isPlanted;
    private bool isWatered;
    private bool isFullyGrown;
    private float currentWateringTime;
    private float currentGrowthTime;
    private GameObject currentGrowthSprite;

    public void PlantSeed()
    {
        if (!isPlanted)
        {
            isPlanted = true;
            currentWateringTime = wateringTime;
            currentGrowthTime = growthTime;
            isFullyGrown = false;
            currentGrowthSprite = Instantiate(growthSprite, transform.position, Quaternion.identity);
            currentGrowthSprite.transform.parent = transform;
        }
    }


    /*public bool IsWatered
    {
        get { return isFullyGrown; }
        set { isFullyGrown = value; }
    }*/

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }

    }*/

    private void Update()
    {
        if (isPlanted && !isFullyGrown)
        {

            currentWateringTime -= Time.deltaTime;
            if (currentWateringTime <= 0f)
            {
                isWatered = true;
            }

            if (isWatered)
            {
                currentGrowthTime -= Time.deltaTime;
                if (currentGrowthTime <= 0f)
                {
                    isFullyGrown = true;

                    // Intenar algun dia una animación de crecimiento, pronto Carlito... :c
                }
            }

        }



    }
}