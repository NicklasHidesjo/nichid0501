using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DartThrower : MonoBehaviour
{
	[SerializeField] GameObject Dart;
	[SerializeField] Transform parent;
	[SerializeField] List<GameObject> darts;

	[SerializeField] Slider forceGauge;

	[Tooltip("The minimum and maximum force allow to throw with")]
	[SerializeField] float minForce = 2f, maxForce = 10f;

	[Tooltip("The amount that force increases by (per second)")]
	[SerializeField] float forceIncrease = 1f;

	float force = 2;

	bool increaseForce = false;
	public bool IncreaseForce { get { return increaseForce; } set { increaseForce = value; } }

	CrossHair aim;

	Game game;


	public void Start()
	{
		aim = FindObjectOfType<CrossHair>();
		game = FindObjectOfType<Game>();
		forceGauge.maxValue = maxForce;
		forceGauge.minValue = minForce;
	}

	public void ThrowDart()
	{
		GameObject newDart = Instantiate(Dart,parent);
		newDart.transform.position = transform.position;
		Vector3 velocity = transform.forward * force;
		newDart.GetComponent<Rigidbody>().velocity = velocity;
		darts.Add(newDart);
	}


	private void Update()
	{
		transform.LookAt(aim.transform);

		HandleInput();
		if (!increaseForce) { return; }
		UpdateForce();
	}

	private void HandleInput()
	{
		if (game.GamePaused)
		{
			return;
		}
		Debug.Log("throw dart");
		if (!game.canThrow)
		{
			return;
		}
		if(Application.platform == RuntimePlatform.Android)
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
					if (increaseForce) { return; }
					force = 2;
					forceIncrease = Mathf.Abs(forceIncrease);
					increaseForce = true;
				}
				if (Input.GetTouch(i).phase == TouchPhase.Ended)
				{
					if (!increaseForce)
					{
						return;
					}
					increaseForce = false;
					ThrowDart();
				}
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(0))
			{
				force = 2;
				forceIncrease = Mathf.Abs(forceIncrease);
				increaseForce = true;
			}
			if (Input.GetMouseButtonUp(0))
			{
				if (!increaseForce)
				{
					return;
				}
				increaseForce = false;
				ThrowDart();
			}
		}
	}

	private void UpdateForce()
	{
		force += forceIncrease * Time.deltaTime;
		if (force > maxForce || force < minForce)
		{
			forceIncrease *= -1;
		}
		forceGauge.value = force;
	}

	public void RemoveDarts()
	{
		foreach (var dart in darts)
		{
			Destroy(dart);
		}
		darts.Clear();
	}
}
