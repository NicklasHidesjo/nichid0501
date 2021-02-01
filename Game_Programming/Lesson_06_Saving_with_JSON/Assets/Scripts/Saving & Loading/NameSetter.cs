using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameSetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] namefields = new TextMeshProUGUI[0];
    [SerializeField] TextMeshProUGUI[] previousNames = new TextMeshProUGUI[0];

    SaveManager saveManager;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        saveManager.LoadData(previousNames.Length);
    }

    public void SaveNames()
    {
        string[] names = new string[namefields.Length];
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = namefields[i].text;
        }
        saveManager.SaveData(names, namefields.Length);
    }

    public void SetNameField(int i, string name)
    {
        previousNames[i].text = name;
    }
}
