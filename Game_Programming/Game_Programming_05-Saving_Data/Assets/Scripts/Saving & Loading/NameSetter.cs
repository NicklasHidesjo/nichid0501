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
        LoadPreviousNames();
    }

    private void LoadPreviousNames()
    {
        string[] names = saveManager.LoadNames(previousNames.Length);
        for (int i = 0; i < previousNames.Length; i++)
        {
            Debug.Log(names[i]);
            previousNames[i].text = names[i];
        }
    }

    public void SaveNames()
    {
        string[] names = new string[namefields.Length];

        for (int i = 0; i < namefields.Length; i++)
        {
            names[i] = namefields[i].text;
        }

        saveManager.SaveNames(names);
    }
}
