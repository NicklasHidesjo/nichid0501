using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameTagManager : MonoBehaviour
{
    public GameObject nameTagPrefab;
    public Vector3 offset;
    public GameObject[] cars;

    internal GameObject[] nameTags;

	bool nameTagsSpawned = false;

    // Start is called before the first frame update
    void Awake()
	{
		FindObjectOfType<SaveManager>().SetNamesToCars();
	}

	public void SpawnNameTags(string[] name)
	{
		nameTags = new GameObject[cars.Length];

		var parent = GameObject.Find("Canvas").transform;

		for (int i = 0; i < nameTags.Length; i++)
		{
			//Spawn a nametag for each player
			nameTags[i] = Instantiate(nameTagPrefab, parent);
		}

		UpdateNameTags(name);
		nameTagsSpawned = true;
	}

	public void UpdateNameTags(string[] name)
	{
		cars[0].transform.name = name[0];
		cars[1].transform.name = name[1];

		for (int i = 0; i < nameTags.Length; i++)
		{
			nameTags[i].GetComponent<TextMeshProUGUI>().text = cars[i].name;
		}
	}

	// Update is called once per frame
	void Update()
    {
		if (!nameTagsSpawned) return;
		for (int i = 0; i < cars.Length; i++)
		{
            nameTags[i].transform.position = Camera.main.WorldToScreenPoint(cars[i].transform.position + offset);
		}
    }
}
