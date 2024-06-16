using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float bubbleSpeed = 1f;
    float xSpeed;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        xSpeed = player.GetLastXDirection() * bubbleSpeed + 10;
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2 (xSpeed, 0f);
    }
}
