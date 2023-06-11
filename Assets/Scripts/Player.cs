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

    public Plant plant;




    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
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
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1f, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                Plant plant = hit.collider.GetComponent<Plant>();
                if (plant != null)
                {
                    plant.PlantSeed();

                }
            }
        }
    }


    void FixedUpdate()
    {
        Vector2 moveForce = PlayerInput * moveSpeed;

        rb.velocity = moveForce;
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