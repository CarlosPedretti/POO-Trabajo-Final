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

        /*if (Input.GetKeyDown(KeyCode.F))
        {
            plant.IsWatered = true;
            plant.PlantSeed();
        }*/


    }


    void FixedUpdate()
    {
        Vector2 moveForce = PlayerInput * moveSpeed;

        rb.velocity = moveForce;
    }
    
   


}