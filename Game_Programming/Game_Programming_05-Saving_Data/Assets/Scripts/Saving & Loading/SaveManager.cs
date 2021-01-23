using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void SaveNames(string[] names)
    {
        for (int i = 0; i < names.Length; i++)
        {
            string saveString = "carname" + (i + 1);
            PlayerPrefs.SetString(saveString, names[i]);
        }
    }

    public string[] LoadNames(int numberOfNames)
    {
        string[] names = new string[numberOfNames];
        for (int i = 0; i < names.Length; i++)
        {
            string loadString = "carname" + (i + 1);
            names[i] = PlayerPrefs.GetString(loadString);
        }
        return names;
    }

    public void SaveCarPos(int index, Vector3 position)
    {
        string saveString = "car" + index + "xPos";
        PlayerPrefs.SetFloat(saveString, position.x);

        saveString = "car" + index + "yPos";
        PlayerPrefs.SetFloat(saveString, position.y);
    }

    public Vector3 LoadCarPos(int index)
    {
        string loadString = "car" + index + "xPos";
        float x = PlayerPrefs.GetFloat(loadString);
        
        loadString = "car" + index + "yPos";
        float y = PlayerPrefs.GetFloat(loadString);

        return new Vector3(x, y, 0);
    }
}
