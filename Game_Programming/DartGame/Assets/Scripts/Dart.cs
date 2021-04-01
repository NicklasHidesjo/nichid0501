using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
	bool collided = false;
	Rigidbody body;

	private void Start()
	{
		body = GetComponentInParent<Rigidbody>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(collided) { return; }
		if (other.gameObject.GetComponent<HitZone>())
		{
			collided = true;
			body.useGravity = false;
			body.velocity = Vector3.zero;
			Debug.Log("Hit board At: " + other.gameObject.GetComponent<HitZone>().Score.ToString() + " \n At position:" + transform.position);
		}
	}
}
