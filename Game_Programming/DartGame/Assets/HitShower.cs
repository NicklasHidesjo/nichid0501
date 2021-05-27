using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitShower : MonoBehaviour
{
	[SerializeField] GameObject hitText;
	[SerializeField] Vector3 offset;
	[SerializeField] List<GameObject> texts;
	public void ShowHit(string hitInfo, Vector3 dartPos)
	{
		Vector3 pos = FindObjectOfType<Camera>().WorldToScreenPoint(dartPos);
		GameObject SpawnObject = Instantiate(hitText, pos + offset, Quaternion.identity,transform);
		SpawnObject.GetComponentInChildren<Text>().text = hitInfo;
		texts.Add(SpawnObject);
	}

	public void RemoveTexts()
	{
		foreach (var text in texts)
		{
			Destroy(text);
		}
		texts.Clear();
	}
}
