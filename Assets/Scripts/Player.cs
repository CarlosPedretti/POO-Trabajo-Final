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

        /*if (canPressF && Input.GetKeyDown(KeyCode.F))
        {
            canPressF = false;
            Debug.Log("Apreté F");
            PlantPlant();

            //private ReceptorPlanta receptorPlanta;
            //receptorPlanta.GetComponent<ReceptorPlanta>();
        }

        if (canPressG && Input.GetKeyDown(KeyCode.G))
        {
            canPressG = false;
            Debug.Log("Apreté G");
            WaterPlant();
        }

        if (canPressF && Input.GetKeyUp(KeyCode.F))
        {
            canPressF = true;
        }

        if (canPressG && Input.GetKeyUp(KeyCode.G))
        {
            canPressG = true;
        }*/

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
                if (receptorPlanta != null)
                {
                    WaterPlant();
                }

            }
        }

    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {

        receptorPlanta = other.GetComponent<ReceptorPlanta>();
        canPressF = true;
            canPressG = true;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        receptorPlanta = other.GetComponent<ReceptorPlanta>();
            canPressF = false;
            canPressG = false;
        
    }*/

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



    //Recolección semillas, todavía en desarrollo

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