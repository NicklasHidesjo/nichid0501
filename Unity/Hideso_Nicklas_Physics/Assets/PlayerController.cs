using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 0f;
    [SerializeField] float jumpForce = 0f;


    Rigidbody2D rb2d;
    Animator animator;
    SpriteRenderer spriteRenderer;

    BoxCollider2D feet;

    Vector2 movement = new Vector2();

    [SerializeField] bool touchingGround = false;
    bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        feet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        if(x == 0)
        {            
            animator.ResetTrigger("walk");
            animator.SetTrigger("idle");

        }
        else
        {
            animator.ResetTrigger("idle");
            animator.SetTrigger("walk");

            if (x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }

        movement = new Vector2(speed * x, rb2d.velocity.y);

        if(Input.GetButtonDown("Jump") && touchingGround)
        {
            canJump = true;
        }
            
    }

    private void FixedUpdate()
    {
        rb2d.velocity = movement;
        if(canJump)
        {
            rb2d.AddForce(Vector2.up * jumpForce);
            canJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        touchingGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingGround = false;
    }
}
