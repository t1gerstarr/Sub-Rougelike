using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;

    Vector2 moveInput;

    Rigidbody2D myRigidBody2D; // Making a variable for the players Rigidbody
    
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>(); // Connecting the variable to the Rigidbody component
    }

    void Update()
    {
        Movement();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Movement()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * movementSpeed, moveInput.y * movementSpeed);
        myRigidBody2D.velocity = playerVelocity;
    }
}
