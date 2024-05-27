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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if(distance < distanceBetween)
        {
            animator.SetBool("isMoving", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            
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
        }
    }
}
