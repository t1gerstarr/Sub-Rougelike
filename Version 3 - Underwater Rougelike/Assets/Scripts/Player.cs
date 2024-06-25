using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator animator; // Accesses the Animator component for my player
    [SerializeField] float movementSpeed = 10f; // Sets the default movement speed of the player
    SpriteRenderer playerSprite;
    Vector2 moveInput; // Gets the vector2 (movement on a 2D plane) and assigns it to the moveInput variable
    private Vector2 previousPosition;
    Rigidbody2D myRigidBody2D; // Making a variable for the players Rigidbody
    public float playerHealth = 100f;
    bool movingHorizontal;
    bool movingVertical;
    public bool isAlive = true;
    bool isDamaged = false; // track when player is hit
    [SerializeField] float destroySpeed = 1;
    [SerializeField] GameObject bubble;
    [SerializeField] Transform bubbleGun;

    private float lastXDirection;
    
    
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
        Game();
    }

    void Game()
    {
        if (isAlive)
        {
            Movement();
            FlipSprite();
            Vector2 currentPosition = transform.position;

            movingHorizontal = Mathf.Abs(currentPosition.x - previousPosition.x) > Mathf.Epsilon;
            movingVertical = Mathf.Abs(currentPosition.y - previousPosition.y) > Mathf.Epsilon;

            if (movingHorizontal)
            {
                lastXDirection = moveInput.x;
            }

            previousPosition = currentPosition;
        }
        else if (!isAlive)
        {
            Dead();
        }
    }

        

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Movement()
    {
        if(!isDamaged)
        {
            Vector2 playerVelocity = new Vector2 (moveInput.x * movementSpeed, moveInput.y * movementSpeed);
            myRigidBody2D.velocity = playerVelocity;

            animator.SetBool("isHorizontal", movingHorizontal);
            animator.SetBool("isVertical", movingVertical);
        }
        
    }

    // This checks if the playe is moving down and to a specific direction (left or right). If so, it rotates the player accordingly.
    void FlipSprite()
    {
        if (moveInput.x != 0)
        {
            playerSprite.flipX = moveInput.x < 0;
        }
        else
        {
            playerSprite.flipX = lastXDirection < 0;
        }

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

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        Debug.Log("Player Health: " + playerHealth);

        if (playerHealth <= 0)
        {
            isAlive = false;
            animator.SetTrigger("isDead");
        }

        // trigger damage animation
        if (!isDamaged)
        {
            StartCoroutine(TriggerDamageAnimation());
        }
    }

    private IEnumerator TriggerDamageAnimation()
    {
        isDamaged = true;
        animator.SetBool("isDamaged", true);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("isDamaged", false);
        isDamaged = false;
    }

    public float GetLastXDirection()
    {
        return lastXDirection;
    }

    public void Dead()
    {
       isAlive = false;
        SceneManager.LoadScene("DeathScreen");
    }
}