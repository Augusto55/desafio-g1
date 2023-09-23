using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] float xSpeed;
    [SerializeField] float ySpeed;
    float playerX;
    float playerY;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerX = Input.GetAxis("Horizontal");
        playerY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(playerX * xSpeed, playerY * ySpeed);
    }
}
