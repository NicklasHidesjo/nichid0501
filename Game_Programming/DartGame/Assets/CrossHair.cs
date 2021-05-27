using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Text gyroSpeed;

    DartThrower thrower;
	private void Start()
	{
        thrower = FindObjectOfType<DartThrower>();
        Input.gyro.enabled = true;
	}

	void FixedUpdate()
    {
        // do not forget to have a check if one can throw a dart here. 

        if (thrower.IncreaseForce) { return; }

        if (Application.platform == RuntimePlatform.Android)
        {
            float xChange = Input.acceleration.x * speed * Time.deltaTime;
            float yChange = Input.acceleration.y * speed * Time.deltaTime;

            float xPos = Mathf.Clamp(transform.position.x + xChange, -0.44f, 0.44f);
            float yPos = Mathf.Clamp(transform.position.y + yChange, 0f, 1f);

            transform.position = new Vector3(xPos, yPos, 1.9f);
        }
        else
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            float x = Mathf.Clamp(mousePos.x, -0.44f, 0.44f);
            float y = Mathf.Clamp(mousePos.y, 0f, 1f);
            transform.position = new Vector3(x, y, 1.9f);
        }
    }
}
