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

        myRigidBody2D.freezeRotation = true;
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

        animator.SetBool("isHorizontal", movingHorizontal);
        animator.SetBool("isVertical", movingVertical);
        

    }

    // This checks if the playe is moving down and to a specific direction (left or right). If so, it rotates the player accordingly.
    void FlipSprite()
    {
        playerSprite.flipX = moveInput.x < 0;

        float rotationAngle = 0;

        if (moveInput.y < 0)
        {
            rotationAngle = moveInput.x < 0 ? 45 : moveInput.x > 0 ? -45 : -145; // ? and : are used in place of 'else if' statements to compact my code and help me avoid too many 'if' statements.
        }
        else if (Input.GetKey(KeyCode.A) && moveInput.y > 0)
        {
            rotationAngle = -20;
        }
        else if (moveInput.y > 0)
        {
            rotationAngle = 20;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}
