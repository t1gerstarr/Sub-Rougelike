using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

    private float distance;

    Animator animator;
    SpriteRenderer spriterenderer;
    Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() // fixed update because I am dealing with physics and need precise updates
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if(distance < distanceBetween)
        {
            animator.SetBool("isMoving", true);
            myRigidBody.velocity = direction * speed;

            if (direction.x < 0)
            {
                spriterenderer.flipX = true;
            }
            else if (direction.x > 0)
            {
                spriterenderer.flipX = false;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
            myRigidBody.velocity = Vector2.zero;
        }
    }
}
