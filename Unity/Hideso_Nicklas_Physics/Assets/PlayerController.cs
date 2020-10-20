using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 0f;
    [SerializeField] float jumpForce = 0f;


    Rigidbody2D rb2d;

    BoxCollider2D feet;

    Vector2 movement = new Vector2();

    [SerializeField] bool touchingGround = false;
    bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        feet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
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
