using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planta : MonoBehaviour
{
    private PlantConfig plantConfig;
    private SpriteRenderer spriteRenderer;

    private bool isWatered;
    private bool isFullyGrown;

    public int currentWaterAmount;
    private int currentWaterNeeded;

    public GameObject plantaUIPrefab;
    public Image[] plantaUIBars;
    public Image plantaUIBar;


    public void Setup(PlantConfig config)
    {
        plantConfig = config;


        plantaUIPrefab = Instantiate(Resources.Load<GameObject>("PlantaUIPrefab"), transform.position, Quaternion.identity, transform);

        plantaUIBars = plantaUIPrefab.GetComponentsInChildren<Image>();
        plantaUIBar = plantaUIBars[1];

        currentWaterNeeded = plantConfig.quantityOfWaterNeeded;

        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = plantConfig.growthSprites[0];
        spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f);
        spriteRenderer.sortingLayerName = "Plants"; 
        spriteRenderer.sortingOrder = 1; 

        isWatered = false;
        isFullyGrown = false;

        currentWaterAmount = 0;

        UpdateWaterUI(0);


    }


    public int Watering(int currentWaterOnWateringCan)
    {
        if (isWatered || isFullyGrown)
        {
            return 0;
        }

        int waterConsumed = Mathf.Min(currentWaterNeeded, currentWaterOnWateringCan);
        currentWaterNeeded -= waterConsumed;
        currentWaterOnWateringCan -= waterConsumed;

        currentWaterAmount += waterConsumed;


        if (currentWaterNeeded <= 0)
        {
            UpdateWateringUI(0);
            StartCoroutine(WateringCoroutine());
        }


        UpdateWaterUI(currentWaterAmount);

        return waterConsumed;
    }

    private void UpdateWaterUI(int currentWaterAmount)
    {
        if (plantaUIBar != null)
        {

            float fillAmount = Mathf.Clamp01((float)currentWaterAmount / (float)plantConfig.quantityOfWaterNeeded);

            plantaUIBar.fillAmount = fillAmount;
        }
    }

    private void UpdateWateringUI(float currentWateringTime)
    {
        if (plantaUIBar != null)
        {

            float fillAmount = Mathf.Clamp01((float)currentWateringTime / (float)plantConfig.wateringTime);

            plantaUIBar.fillAmount = fillAmount;
            Color WateringColor = new Color(83f / 255f, 181f / 255f, 255f / 255f);
            plantaUIBar.color = WateringColor;
        }
    }

    IEnumerator WateringCoroutine()
    {
        isWatered = true;
        float currentWateringTime = 0f;

        while (currentWateringTime < plantConfig.wateringTime)
        {
            currentWateringTime += Time.deltaTime;

            UpdateWateringUI(currentWateringTime);

            yield return null;
        }

        if (!isFullyGrown)
        {
            UpdateGrowthUI(0);
            StartCoroutine(GrowingCoroutine());

        }

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

            }

            UpdateGrowthUI(currentGrowthTime);

            yield return null;
        }


        isFullyGrown = true;
        Debug.Log("La planta está completamente desarrollada y lista para ser cosechada");
    }

    private void UpdateGrowthUI(float currentGrowthTime)
    {
        if (plantaUIBar != null)
        {

            float fillAmount = Mathf.Clamp01((float)currentGrowthTime / (float)plantConfig.wateringTime);

            plantaUIBar.fillAmount = fillAmount;
            Color GrowColor = new Color(53f / 255f, 255f / 255f, 0f / 255f);
            plantaUIBar.color = GrowColor;
        }
    }


    public bool IsFullyGrown
    {
        get { return isFullyGrown; }
    }


    public void Harvesting()
    {
        Destroy(gameObject);

        PlantInventoryManager.CollectPlant(plantConfig.plantName, 1);
    }
}
