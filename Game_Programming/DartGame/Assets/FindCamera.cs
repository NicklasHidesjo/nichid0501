using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCamera : MonoBehaviour
{
	private void Start()
	{
		GetComponent<Canvas>().worldCamera = Camera.main;
	}
}
