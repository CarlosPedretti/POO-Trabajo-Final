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
    public float forceDamping;
    Animator animator;
    public bool canMove = true;
    [SerializeField] private Vector2 velocityRebound;






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
        
      
    }


    void FixedUpdate()
    {
        if(canMove)
        {
            Vector2 moveForce = PlayerInput * moveSpeed;
            rb.velocity = moveForce;
        }
       
    }

    public void Rebound(Vector2 punchPoint)
    {
        rb.velocity = new Vector2(-velocityRebound.y * punchPoint.y, velocityRebound.x);
    }



}