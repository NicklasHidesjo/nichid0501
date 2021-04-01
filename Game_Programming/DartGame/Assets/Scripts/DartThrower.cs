using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DartThrower : MonoBehaviour
{
	[SerializeField] GameObject Dart;
	[SerializeField] Slider force;
	[SerializeField] Text forceText;

	[SerializeField] Slider yAngle;
	[SerializeField] Slider xAngle;
	[SerializeField] Text xAngleText;
	[SerializeField] Text yAngleText;

	[SerializeField] Transform parent;

	public void Start()
	{
		UpdateLaunchRotation();
		UpdateText();
	}

	public void ThrowDart()
	{
		GameObject newDart = Instantiate(Dart,parent);
		newDart.transform.position = transform.position;
		Vector3 velocity = transform.forward * force.value;
		newDart.GetComponent<Rigidbody>().velocity = velocity;
	}

	public void UpdateText()
	{
		float x = (float)Math.Round(xAngle.value, 2);
		float y = (float)Math.Round(yAngle.value, 2);
		float speed = (float)Math.Round(force.value, 2);

		xAngleText.text = "Angle X: " + x;
		yAngleText.text = "Angle Y: " + y;
		forceText.text = "Force: " + speed;
	}

	public void UpdateLaunchRotation()
	{
		Quaternion target = Quaternion.Euler(-xAngle.value, yAngle.value, 0);
		transform.rotation = target;
	}
}
