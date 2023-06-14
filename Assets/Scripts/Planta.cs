using System.Collections;
using UnityEngine;

public class Planta : MonoBehaviour
{
    private PlantConfig plantConfig;
    private SpriteRenderer spriteRenderer;
    private Coroutine wateringCoroutine;

    private bool isWatered;
    private bool isFullyGrown;

    public void Setup(PlantConfig config)
    {
        plantConfig = config;
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = plantConfig.growthSprites[0];
        spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f);
        spriteRenderer.sortingLayerName = "Plants"; // Asigna el sorting layer deseado para las plantas
        spriteRenderer.sortingOrder = 1; // Asigna el SortingOrder como 1 para todas las plantas
        isWatered = false;
        isFullyGrown = false;
    }

    public void Watering()
    {
        if (isWatered)
        {
            return;
        }

        if (wateringCoroutine == null)
        {
            wateringCoroutine = StartCoroutine(WateringCoroutine());
        }
    }

    IEnumerator WateringCoroutine()
    {
        isWatered = true;
        float currentWateringTime = 0f;

        while (currentWateringTime < plantConfig.wateringTime)
        {
            currentWateringTime += Time.deltaTime;
            Debug.Log("Regando la planta...");

            yield return null;
        }

        if (!isFullyGrown)
        {
            StartCoroutine(GrowingCoroutine());
        }

        Debug.Log("La planta ha sido regada");
        wateringCoroutine = null;
    }

    IEnumerator GrowingCoroutine()
    {
        float currentGrowthTime = 0f;

        while (currentGrowthTime < plantConfig.growthTime)
        {
            currentGrowthTime += Time.deltaTime;

            int spriteIndex = Mathf.RoundToInt((currentGrowthTime / plantConfig.growthTime) * (plantConfig.growthSprites.Length - 1));

            if (spriteIndex >= 0 && spriteIndex < plantConfig.growthSprites.Length)
            {
                spriteRenderer.sprite = plantConfig.growthSprites[spriteIndex];

                //spriteRenderer.transform.localScale = new Vector3(5f, 5f, 1f); // Ajusta los valores según tu necesidad
            }

            yield return null;
        }

        isFullyGrown = true;
        Debug.Log("La planta está completamente desarrollada y lista para ser cosechada");
    }

    public void Harvesting()
    {
        Destroy(gameObject);
    }
}
