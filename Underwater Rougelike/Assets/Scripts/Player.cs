using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Animator animator; // Accesses the Animator component for my player
    [SerializeField] float movementSpeed = 10f; // Sets the default movement speed of the player
    SpriteRenderer playerSprite;
    Vector2 moveInput; // Gets the vector2 (movement on a 2D plane) and assigns it to the moveInput variable
    private Vector2 previousPosition;
    Rigidbody2D myRigidBody2D; // Making a variable for the players Rigidbody

    bool movingHorizontal;
    bool movingVertical;
    
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>(); // Initialises the Rigidbody2D variable
        animator = GetComponent<Animator>(); // Initialises the Animator variable
        previousPosition = transform.position; // Initialises the previousPosition with the inital position of player
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Movement();
        FlipSprite();
        Vector2 currentPosition = transform.position;

        movingHorizontal = Mathf.Abs(currentPosition.x - previousPosition.x) > Mathf.Epsilon;
        movingVertical = Mathf.Abs(currentPosition.y - previousPosition.y) > Mathf.Epsilon;

        previousPosition = currentPosition;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Movement()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * movementSpeed, moveInput.y * movementSpeed);
        myRigidBody2D.velocity = playerVelocity;

        animator.SetBool("isMovingH", movingHorizontal);
        animator.SetBool("isMovingV", movingVertical);
    }

    void FlipSprite()
    {
        if (moveInput.x > 0)
        {
            playerSprite.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            playerSprite.flipX = true;
        }

        if (moveInput.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
