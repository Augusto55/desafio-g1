using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] float xSpeed;
    [SerializeField] float ySpeed;
    float playerX;
    float playerY;

    float health;
    float speed;

    Rigidbody2D rb;
    Animator anim;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootingPosition;
    public float bulletSpeed = 25;
    Vector2 lookDirection;
    float lookAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();



        health = 3;
    }

    void Update()
    {
        Move();
        Shooting();
        Flip();

        anim.SetInteger("speed", (int)playerX);

    }

    void Move()
    {
        playerX = Input.GetAxis("Horizontal");
        playerY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(playerX * xSpeed, playerY * ySpeed);

        speed = Mathf.Abs(playerX); // Use Mathf.Abs para obter um valor positivo
        Debug.Log(playerX);
    }

    void Shooting()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lookDirection = mousePosition - (Vector2)shootingPosition.position;
        lookDirection.Normalize();

        lookAngle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;

        shootingPosition.rotation = Quaternion.Euler(0, 0, lookAngle);



        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletClone = Instantiate(bullet);
            bulletClone.transform.position = shootingPosition.position;

            float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            Debug.Log(lookDirection);

            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

            bulletClone.GetComponent<Rigidbody2D>().velocity = lookDirection * bulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            {
            Debug.Log(health);
            health -= 1;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Flip()
    {
        if (lookDirection.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (lookDirection.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }


}
