using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		int hit = 0;
		bool doubleHit = false;
		if(other.gameObject.GetComponent<Frame>())
		{
			collided = true;
			Vector3 vel = body.velocity;
			body.velocity = Vector3.zero;
			body.AddForce(vel, ForceMode.Impulse);
			GetComponent<BoxCollider>().enabled = false;
			GetComponent<CapsuleCollider>().enabled = false;

			FindObjectOfType<HitShower>().ShowHit("Hit Frame", transform.position);
		} 
		if (other.gameObject.GetComponent<HitZone>())
		{
			collided = true;
			body.useGravity = false;
			body.velocity = Vector3.zero;
			hit = other.gameObject.GetComponent<HitZone>().Score;
			doubleHit = other.gameObject.GetComponent<HitZone>().DoubleScore;
			string HitInfo = "Hit: " + hit.ToString();
			FindObjectOfType<HitShower>().ShowHit(HitInfo, transform.position);
		}

		FindObjectOfType<Game>().ThrowDart(hit, doubleHit, transform.position);
	}
}
