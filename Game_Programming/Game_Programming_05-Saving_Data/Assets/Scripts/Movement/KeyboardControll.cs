using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControll : MonoBehaviour
{
    Mover mover;
    void Start()
    {
        mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");

        mover.Move(new Vector3(movX, movY, 0));
    }
}
