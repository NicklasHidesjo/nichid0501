using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Text gyroSpeed;
    Quaternion inverseFlat;

    Quaternion tilt;

    DartThrower thrower;
    Game game;
    
    private void Start()
	{
        game = FindObjectOfType<Game>();
        thrower = FindObjectOfType<DartThrower>();
        Input.gyro.enabled = true;
        inverseFlat = Quaternion.Inverse(Input.gyro.attitude);
	}

	void FixedUpdate()
    {
        if (!game.CanThrow) { return; }
        if (thrower.IncreaseForce) { return; }

        if (Application.platform == RuntimePlatform.Android)
		{
			tilt = Input.gyro.attitude * inverseFlat;

			gyroSpeed.text = "X: " + tilt.x + " | Y: " + tilt.y + " | Z: " + tilt.z;

			float xChange = tilt.x * speed * Time.fixedDeltaTime;
			float yChange = tilt.y * speed * Time.fixedDeltaTime;

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
