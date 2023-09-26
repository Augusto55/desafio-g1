using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Vector2 mousePosition;
    private Vector2 objectPosition;

    [SerializeField] public SpriteRenderer weaponSpriteRenderer;

    bool isFacingRight;
    bool isReloading;
    bool isShooting;

    Animator anim;

    SpriteRenderer spriteRenderer;

    public bool boxInteraction = false;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.isFacingRight = GameObject.Find("Player").GetComponent<NewBehaviourScript>().isFacingRight;

    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer.enabled = false;
    }

    void Update()
    {
        RotateSprite();
        this.isFacingRight = GameObject.Find("Player").GetComponent<NewBehaviourScript>().isFacingRight;
        this.isReloading = GameObject.Find("Player").GetComponent<NewBehaviourScript>().isReloading;
        this.isShooting = GameObject.Find("Player").GetComponent<NewBehaviourScript>().isShooting;

        anim.SetBool("isReloading", isReloading);
        anim.SetBool("isShooting", isShooting);

        Debug.Log(boxInteraction);

        if (boxInteraction)
        {
            spriteRenderer.enabled = true;
        }

    }




    void RotateSprite()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objectPosition = transform.position;

        float angle = Mathf.Atan2(mousePosition.y - objectPosition.y, mousePosition.x - objectPosition.x) * Mathf.Rad2Deg;

        if (isFacingRight)
        {
            weaponSpriteRenderer.flipX = false;
        }
        else
        {
            weaponSpriteRenderer.flipX = true;
            angle += 180f;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    


    }



 







