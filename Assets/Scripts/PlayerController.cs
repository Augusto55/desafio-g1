using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] float xSpeed;
    [SerializeField] float ySpeed;

    float playerX;
    float playerY;

    float health = 3;
    float speed;

    Rigidbody2D rb;
    Animator anim;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI bulletsLeft;
    int bulletNumber = 7;
    public bool isReloading = false;
    public bool isShooting;
    float timer;
    [SerializeField] float shotTimer;

    [SerializeField] GameObject bullet;
    public GameObject gun;
    [SerializeField] Transform shootingPosition;
    public float bulletSpeed = 25;
    Vector2 lookDirection;
    float lookAngle;


    public bool isFacingRight = false;

    public GameOver gameOver;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthText.text = health.ToString();
        bulletsLeft.text = bulletNumber.ToString();
    }

    void Update()
    {
        Move();
        Shooting();
        Flip();
        Reload();
        if (isShooting)
        {
            ResetShootingFlag();
        }
        anim.SetInteger("speed", (int)speed);
        UpdateBullets();


    }

    void Move()
    {
        playerX = Input.GetAxis("Horizontal");
        playerY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(playerX * xSpeed, playerY * ySpeed);

        speed = new Vector2(playerX, playerY).magnitude; 
    }

    void Shooting()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lookDirection = mousePosition - (Vector2)shootingPosition.position;
        lookDirection.Normalize();

        lookAngle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;

        shootingPosition.rotation = Quaternion.Euler(0, 0, lookAngle);



        if (Input.GetMouseButtonDown(0) && !isReloading && Time.time > timer && gun.GetComponent<Gun>().boxInteraction)
        {   
            UpdateBullets();
            timer = Time.time + shotTimer;
            isShooting = true;  
            ResetShootingFlag();
            GameObject bulletClone = Instantiate(bullet);
            bulletClone.transform.position = shootingPosition.position;

            float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

            bulletClone.GetComponent<Rigidbody2D>().velocity = lookDirection * bulletSpeed;

            bulletNumber -= 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            {
            health -= 1;
           UpdateHealth();

            if (health <= 0)
            {
                gameOver.Setup();
                Destroy(gameObject);
            }
        }
    }

    private void Flip()
    {
        if (lookDirection.x > 0.25f)
        {
            transform.eulerAngles = new Vector2(0, 0);
            isFacingRight = true;
        }
        else if (lookDirection.x < -0.25f)
        {
            transform.eulerAngles = new Vector2(0, 180);
            isFacingRight = false;
        }
    }

    void Reload()
    {    
        if((bulletNumber == 0 || Input.GetKeyDown(KeyCode.R)) && gun.GetComponent<Gun>().boxInteraction)
        {
            if (!isReloading)
            {
                StartCoroutine(ReloadDelay());
            }
        }
    }

    IEnumerator ReloadDelay()
    {
        isReloading = true;

        yield return new WaitForSeconds(2);

        bulletNumber = 7;

        isReloading = false;
    }

    IEnumerator ResetShootingFlag()
    {
        yield return new WaitForSeconds(0.8f);

        isShooting = false;
    }

    private void UpdateHealth()
    {
        healthText.text = health.ToString();
    }

    void UpdateBullets()
    {   
        bulletsLeft.text = bulletNumber.ToString();
        if (isReloading)
        {
            bulletsLeft.text = "Reloading";
        }
    }
}
