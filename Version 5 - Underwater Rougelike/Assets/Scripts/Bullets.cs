using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public float damage;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        BulletSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BulletSetUp()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemies"))
        {
            Enemies enemy = other.GetComponent<Enemies>();

            if(enemy != null)
            {
                enemy.health -= damage;
                Debug.Log("Enemy Health: " + enemy.health);

                if (enemy.health <= 0)
                {
                    Destroy(other.gameObject);
                }
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("LvlBorder"))
        {
            Destroy(gameObject); // Destroys bullets when they hit the level borders
        }
    }
}
