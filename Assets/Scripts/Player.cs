using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 PlayerInput;
    float horizontalMov;
    float verticalMov;
    Animator animator;

    public int wateringCanCapacity;
    public int currentWaterOnWateringCan;

    public ReceptorPlanta receptorPlanta;
    public Water waterObject;



    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 moveForce = PlayerInput * moveSpeed;
        rb.velocity = moveForce;


    }



    void Update()
    {
        horizontalMov = Input.GetAxisRaw("Horizontal");
        verticalMov = Input.GetAxisRaw("Vertical");
        PlayerInput = new Vector2(horizontalMov, verticalMov).normalized;
        animator.SetFloat("Horizontal", horizontalMov);
        animator.SetFloat("Vertical", verticalMov);
        animator.SetFloat("Speed", PlayerInput.sqrMagnitude);


        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 0.5f, Vector2.zero);

            Debug.Log("Apreté F");

            foreach (RaycastHit2D hit in hits)
            {
                receptorPlanta = hit.collider.GetComponent<ReceptorPlanta>();
                if (receptorPlanta != null)
                {
                    PlantPlant();
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 0.5f, Vector2.zero);

            Debug.Log("Apreté G");

            foreach (RaycastHit2D hit in hits)
            {
                receptorPlanta = hit.collider.GetComponent<ReceptorPlanta>();
                if (receptorPlanta != null && currentWaterOnWateringCan > 0)
                {
                    WaterPlant();

                }

                waterObject = hit.collider.GetComponent<Water>();
                if (waterObject != null)
                {
                    if (currentWaterOnWateringCan < wateringCanCapacity)
                    {
                        CollectWater();
                    }
                }

            }
        }

    }


    private void PlantPlant()
    {
        if (receptorPlanta != null && receptorPlanta.IsPlantable())
        {
            receptorPlanta.Plant();
        }
    }

    private void WaterPlant()
    {
        if (receptorPlanta != null && currentWaterOnWateringCan > 0)
        {
            Planta planta = receptorPlanta.GetComponentInChildren<Planta>();
            if (planta != null)
            {
                int waterConsumed = planta.Watering(currentWaterOnWateringCan);
                currentWaterOnWateringCan -= waterConsumed;
                Debug.Log("Me acabo de ejecutar WaterPlant(). Agua consumida: " + waterConsumed);

                // Asegurarse de que currentWaterOnWateringCan no sea negativo
                currentWaterOnWateringCan = Mathf.Max(0, currentWaterOnWateringCan);
                GameManager.currentWaterOnWateringCan = currentWaterOnWateringCan;
            }
        }
        GameManager.Instance.UpdateWaterUI();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 difference = transform.position - collision.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }



    private void CollectWater()
    {
        if (waterObject != null)
        {
            int waterAmount = waterObject.CurrentWaterAmount;
            int remainingCapacity = wateringCanCapacity - currentWaterOnWateringCan;

            if (waterAmount <= remainingCapacity)
            {
                waterObject.CurrentWaterAmount -= waterAmount; // Restar la cantidad de agua recogida de la fuente
                currentWaterOnWateringCan += waterAmount; // Sumar la cantidad de agua recogida a la cantidad actual en la regadera
                GameManager.currentWaterOnWateringCan = currentWaterOnWateringCan;

                Debug.Log("Recogí " + waterAmount + " de agua.");
            }
            else
            {
                if (remainingCapacity > 0)
                {
                    waterObject.CurrentWaterAmount -= remainingCapacity; // Restar la cantidad de agua recogida de la fuente
                    currentWaterOnWateringCan += remainingCapacity; // Sumar la cantidad de agua recogida a la cantidad actual en la regadera
                    GameManager.currentWaterOnWateringCan = currentWaterOnWateringCan;

                    Debug.Log("Recogí " + remainingCapacity + " de agua.");
                }
                else
                {
                    Debug.Log("La regadera está llena, no puedes recoger más agua.");
                }
            }

            GameManager.Instance.UpdateWaterUI();
        }
    }



    //Recolección semillas

    private Dictionary<string, int> seedInventory = new Dictionary<string, int>();

    public void CollectSeed(string seedType, int amount)
    {
        if (seedInventory.ContainsKey(seedType))
        {
            seedInventory[seedType] += amount;
        }
        else
        {
            seedInventory[seedType] = amount;
        }

        GameManager.Instance.UpdateSeedUI(seedType, GetSeedCount(seedType));



    }

    public int GetSeedCount(string seedType)
    {
        if (seedInventory.ContainsKey(seedType))
        {
            return seedInventory[seedType];
        }
        else
        {
            return 0;
        }
    }


}