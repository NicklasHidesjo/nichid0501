using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosSetter : MonoBehaviour
{
	public void SetPos(Vector3 pos)
	{
		transform.position = FindObjectOfType<Camera>().WorldToScreenPoint(pos);
	}
}
