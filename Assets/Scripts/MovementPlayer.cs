using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MovementPlayer : MonoBehaviour
{

    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 forceToApply;
    public Vector2 PlayerInput;
    float horizontalMov;
    float verticalMov;

    public float forceDamping;





    //Animations

    Animator animator;
    string currentState;
    const string PLAYER_WalkUp = "Player_FrontWalk";
    const string PLAYER_IdleUp = "Player_FrontIdle";

    const string PLAYER_WalkDown = "Player_BackWalk";
    const string PLAYER_IdleDown = "Player_BackIdle";

    const string PLAYER_WalkLeft = "Player_LeftIdle";
    const string PLAYER_IdleLeft = "Player_LeftIdle";

    const string PLAYER_WalkRight = "Player_RightIdle";
    const string PLAYER_IdleRight = "Player_RightIdle";
        

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    void Update()
    {

        horizontalMov = Input.GetAxisRaw("Horizontal");
        verticalMov = Input.GetAxisRaw("Vertical");
        PlayerInput = new Vector2(horizontalMov, verticalMov).normalized;
    }


    void FixedUpdate()
    {
        Vector2 moveForce = PlayerInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
        rb.velocity = moveForce;

        if (horizontalMov > 0)
        {
            ChangeAnimationState(PLAYER_WalkRight);
        }



    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            forceToApply += new Vector2(-20, 0);
            Destroy(collision.gameObject);
        }
    }



    void ChangeAnimationState(string newState)
    {
        if (currentState == newState);

        animator.Play(newState);

        currentState = newState;
    }
}