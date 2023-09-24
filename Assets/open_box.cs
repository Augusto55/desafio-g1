using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_box : MonoBehaviour
{
    bool haveGun = false;
    bool boxClean = true;
    public Sprite novaImagem;

    void Start()
    {
        
    }
    void Update()
    {
        Debug.Log(haveGun + "--" + boxClean);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (collision.gameObject.tag == "Player")
        {
            haveGun = true;
            boxClean = false;
            spriteRenderer.sprite = novaImagem;
        }
    }
}