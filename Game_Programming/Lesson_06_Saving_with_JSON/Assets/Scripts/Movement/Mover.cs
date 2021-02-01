using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    Rigidbody2D rb2d;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector3 movement)
    {
        movement = movement.normalized;
        
        if(movement.sqrMagnitude > 0.01f)
        {
            transform.up = movement;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

            rb2d.AddForce(transform.up * speed);
        }

    }
}
