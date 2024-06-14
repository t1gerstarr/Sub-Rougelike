using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    // Public variables to use on ALL enemies
    public GameObject player;
    public float speed;
    public float distanceToAttack;
    [SerializeField] float seperationRadius; // min distance between enemies
    [SerializeField] float seperationStrength; // strength of the force pushing them away from each other
    public bool damageDealt = false;

    // Private variables to use on ALL enemies
    private float distance;
    private bool followPlayer = false;

    // Enemy type variable
    public EnemyType enemyType;
    

    // ExploFish variables
    [SerializeField] float destroySpeed;

    Animator animator;
    SpriteRenderer spriterenderer;
    Rigidbody2D myRigidBody;

    // Player Script
    private Player playerscript; 

    public enum EnemyType
    {
        ExploFish
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();

        // Find player gameobject and access script
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerscript = player.GetComponent<Player>();
        }

        // Start coroutine to wait before following player
        StartCoroutine(WaitBeforeFollowing()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer && playerscript.isAlive)
        {
            EnemyMove();
            ExploFishAttack();
        }
    }

    IEnumerator WaitBeforeFollowing()
    {
        yield return new WaitForSeconds(2);
        followPlayer = true;
    }

    void EnemyMove()
    {
        if (player == null) return; // returns value when player = null (player is dead)

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if(distance > distanceToAttack)
        {
            animator.SetBool("isMoving", true);
            
            // seperation between enemies
            Vector2 seperationForce = Vector2.zero;
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemies");
            foreach (GameObject enemy in allEnemies)
            {
                if (enemy != gameObject)
                {
                    Vector2 toOtherEnemy = transform.position - enemy.transform.position;
                    float distanceToOtherEnemy = toOtherEnemy.magnitude;

                    if (distanceToOtherEnemy > 0 && distanceToOtherEnemy < seperationRadius)
                    {
                        seperationForce += toOtherEnemy.normalized / distanceToOtherEnemy;
                    }
                }
            }

            // Combine movement towards player and seperation force
            Vector2 combinedForce = direction * speed + seperationForce * seperationStrength;

            // making sure it is not divided by 0
            if (!float.IsNaN(combinedForce.x) && !float.IsNaN(combinedForce.y))
            {
                myRigidBody.velocity = combinedForce;
            }
            else
            {
                myRigidBody.velocity = Vector2.zero;
            }

            // Flip enemy sprite
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
        if (player == null) return ;

        if (enemyType == EnemyType.ExploFish && distance < distanceToAttack && !damageDealt)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);

            //Access Player Health
            if (playerscript != null)
            {
                playerscript.TakeDamage(10f);
                damageDealt = true;

                if (playerscript.playerHealth <= 0)
                {
                    playerscript.isAlive = false;
                }
            }

            Destroy(gameObject, destroySpeed);
        }
    }
}
