using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box : MonoBehaviour
{
    bool haveGun = false;
    bool boxClean = true;
    public Sprite novaImagem;
    public GameObject player;
    public GameObject gun;

    void Start()
    {

    }
    void Update()
    {
       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (collision.gameObject.tag == "Player")
        {
            haveGun = true;
            boxClean = false;
            spriteRenderer.sprite = novaImagem;
            gun.GetComponent<Gun>().boxInteraction = true;
        }
    }
}
