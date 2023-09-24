using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    float health = 2; 

    private float distance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {   
        target = GameObject.Find("Player").transform;
    }


    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shot"))
        {
            health -= 1;

            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
