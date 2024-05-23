using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

    // This creates some Bools to check the direction of the player. It then uses these Bools through a number of IF Statements to check how the player is moving and rotate the sprite accordingly.
    void FlipSprite()
    {
        bool moveLeft = moveInput.x < 0;
        bool moveDown = moveInput.y < 0;
        bool moveUp = moveInput.y > 0;
        
        playerSprite.flipX = moveLeft; // Flips the sprite along the X-axis when moveLeft is True

        if (moveLeft && moveDown)
        {
            transform.rotation = Quaternion.Euler(0, 0, 145);
        }
        else if (moveDown)
        {
            transform.rotation = Quaternion.Euler(0, 0, -145);
        }
        else if (moveUp)
        {
            transform.rotation = Quaternion.Euler(0, 0, 20);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
