using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("This will set what type of movement we are going " +
        "to use /n 1 = Translate movement /n 2 = Force movement " +
        "/n 3 = Velocity movement")]
    [SerializeField] MovementTypes movementType = MovementTypes.translateMovement;
    [SerializeField] float speed = 100f;
    
    
    Rigidbody2D rb2d;
    AnimationController animController;


    float horizontalMovement = 0;
    float verticalMovement = 0;
    Vector2 movement = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animController = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        SetAnimation();
    }

    void MoveCharacter()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        switch (movementType)
        {
            case MovementTypes.translateMovement:
                TranslateMovement();
                break;
            case MovementTypes.forceMovement:
                ForceMovement();
                break;
            case MovementTypes.velocityMovement:
                VelocityMovement();
                break;

            default:
                Debug.Log("you forgot to set movementType");
                break;
        }
    }

    // the different movement types that exist.
    void TranslateMovement()
    {
        movement = new Vector2(horizontalMovement, verticalMovement);

        if (movement.SqrMagnitude() > 1)
        {
            movement.Normalize();
        }

        movement = (movement * speed) * Time.deltaTime;

        transform.Translate(movement * Time.deltaTime);
    }
    void ForceMovement()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        movement = new Vector2(horizontalMovement, verticalMovement);

        rb2d.AddForce((movement * speed) * Time.deltaTime);
    }
    void VelocityMovement()
    {
        movement = new Vector2(horizontalMovement, verticalMovement) * speed * Time.deltaTime;

        rb2d.velocity = movement;
    }

    void SetAnimation()
    {
        if(horizontalMovement > 0)
        {
            animController.setAnimation("WalkRight");
        }
        else if (horizontalMovement < 0)
        {
            animController.setAnimation("WalkLeft");
        }

        if(verticalMovement > 0)
        {
            animController.setAnimation("WalkUp");
        }
        else if(verticalMovement < 0)
        {
            animController.setAnimation("WalkDown");
        }

        else if (horizontalMovement == 0 && verticalMovement == 0)
        {
            animController.setAnimation("Idle");
        }
    }
}
