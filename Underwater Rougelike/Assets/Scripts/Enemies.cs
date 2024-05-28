using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    // Public variables to use on ALL enemies
    public GameObject player;
    public float speed;
    public float distanceToFollow;
    public float distanceToAttack;

    // Private variables to use on ALL enemies
    private float distance;

    // ExploFish variables
    [SerializeField] float destroySpeed;

    Animator animator;
    SpriteRenderer spriterenderer;
    Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        ExploFishAttack();
    }

    void EnemyMove()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if(distance < distanceToFollow && distance > distanceToAttack)
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

    void ExploFishAttack()
    {
        if (tag == "ExploFish" && distance < distanceToAttack)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);

            Destroy(gameObject, destroySpeed);
        }
    }
}
