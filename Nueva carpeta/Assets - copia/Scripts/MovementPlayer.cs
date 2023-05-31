using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MovementPlayer : MonoBehaviour
{

    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 PlayerInput;
    float horizontalMov;
    float verticalMov;
    public float forceDamping;
    Animator animator;





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
       Vector2 moveForce = PlayerInput * moveSpeed;
       rb.velocity = moveForce;
      
    }
    
   


}