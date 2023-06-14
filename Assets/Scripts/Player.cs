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

    public ReceptorPlanta receptorPlanta;

    private bool canPressF;
    private bool canPressG;

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

        if (canPressF && Input.GetKeyDown(KeyCode.F))
        {
            canPressF = false;
            Debug.Log("aprete F");
            PlantPlant();
        }

        if (canPressG && Input.GetKeyDown(KeyCode.G))
        {
            canPressG = false;
            Debug.Log("aprete G");
            WaterPlant();
        }

        if (canPressF && Input.GetKeyUp(KeyCode.F))
        {
            canPressF = true;
        }

        if (canPressG && Input.GetKeyUp(KeyCode.G))
        {
            canPressG = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     
            canPressF = true;
            canPressG = true;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        
            canPressF = false;
            canPressG = false;
        
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
        if (receptorPlanta != null)
        {
            Planta planta = receptorPlanta.GetComponentInChildren<Planta>();
            if (planta != null)
            {

                
                    planta.Watering();
                    Debug.Log("Me acabo de ejecutar WaterPlant()");
                

            }
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

        Debug.Log("Semilla de tipo " + seedType + " recolectada. Cantidad actual: " + seedInventory[seedType]);
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