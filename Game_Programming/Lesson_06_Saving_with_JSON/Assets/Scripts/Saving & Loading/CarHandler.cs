using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarHandler : MonoBehaviour
{
    [SerializeField] GameObject nameTagPrefab = null;

    [SerializeField] Transform[] cars = new Transform[0];
    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);

    GameObject[] nameTags;
    Canvas canvas;
    Camera mainCamera;
    SaveManager saveManager;

    private void Awake()
    {
        GetReferences();
        LoadNameTags();
    }

    private void Start()
    {
        saveManager.LoadData();
    }

    private void GetReferences()
    {
        canvas = FindObjectOfType<Canvas>();
        mainCamera = FindObjectOfType<Camera>();
        saveManager = FindObjectOfType<SaveManager>();
    }

    private void LoadNameTags()
    {
        nameTags = new GameObject[cars.Length];
        for (int i = 0; i < nameTags.Length; i++)
        {
            nameTags[i] = Instantiate(nameTagPrefab, canvas.transform);
        }
    }

    public void SetNameTag(int i, string name)
    {
        nameTags[i].GetComponent<TextMeshProUGUI>().text = name;
    }

    void Update()
    {
        SetNameTagPosition();
    }
    private void SetNameTagPosition()
    {
        for (int i = 0; i < nameTags.Length; i++)
        {
            nameTags[i].transform.position = mainCamera.WorldToScreenPoint(cars[i].position + offset);
        }
    }


/*    public void SaveCarPos()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            saveManager.SaveCarPos(i, cars[i].position);
        }
    }
    public void LoadCarPos()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            cars[i].position = saveManager.LoadCarPos(i);
        }
    }*/
}
