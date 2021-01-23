using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControll : MonoBehaviour
{
    [SerializeField] int mouseFollowPrecision = 500;

    Camera mainCamera;
    Mover mover;
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        mover = GetComponent<Mover>();
    }


    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;

        if(Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            movement = mousePos - mainCamera.WorldToScreenPoint(transform.position);
        }

        if(movement.sqrMagnitude < mouseFollowPrecision)
        {
            movement = Vector3.zero;
        }

        mover.Move(movement);
    }
}
