using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    bool isDead = false;
    Animator anim;
    float health = 2; 

    private float distance;
    public bool IsDead { get => isDead; set => isDead = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        target = GameObject.Find("Player").transform;
    }


    void Update()
    {   

        anim.SetBool("isDead", isDead);

        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
        Flip();
    }

    private void FixedUpdate()
    {
        if (target && !isDead)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;

        } else
        {
            rb.velocity = Vector2.zero;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shot"))
        {
            health -= 1;

            if(health <= 0)
            {
                isDead = true;
                Destroy(gameObject, 2);
            }
        }
    }

    private void Flip()
    {
        if (moveDirection.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (moveDirection.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private void OnDestroy()
    { 
        GameManager.INSTANCE.UpdatePoints(1);
    }

}
