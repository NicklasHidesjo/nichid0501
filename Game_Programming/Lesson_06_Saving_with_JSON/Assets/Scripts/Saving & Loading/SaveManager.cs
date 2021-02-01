using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using System.IO;
using System.Text;
using System.Net;

[Serializable]
public class MultiplePlayers
{
    public PlayerInfo[] players;
}

[Serializable]
public class PlayerInfo
{
    public string Name;
    public Vector3 Position;
}

public class SaveManager : MonoBehaviour
{
    public void SaveData()
    {
        //Get player info
        var players = FindObjectsOfType<Mover>();

        //Create holder object
        var multiplePlayers = new MultiplePlayers();
        multiplePlayers.players = new PlayerInfo[players.Length];

        //put info in playerinfo class
        for (int i = 0; i < players.Length; i++)
        {
            multiplePlayers.players[i] = new PlayerInfo();
            multiplePlayers.players[i].Position = players[i].transform.position;
            multiplePlayers.players[i].Name = players[i].name;
        }

        //turn class into json 
        string jsonString = JsonUtility.ToJson(multiplePlayers);

        SaveToFile("CarGameSaveFile", jsonString);      
        SaveOnline("CarGameSaveFile", jsonString);
    }
    public void SaveData(string[] names, int players)
    {
        //Create holder object
        var multiplePlayers = new MultiplePlayers();
        multiplePlayers.players = new PlayerInfo[players];

        //put info in playerinfo class
        for (int i = 0; i < players; i++)
        {
            multiplePlayers.players[i] = new PlayerInfo();
            multiplePlayers.players[i].Name = names[i];
        }

        //turn class into json 
        string jsonString = JsonUtility.ToJson(multiplePlayers);

        SaveToFile("CarGameSaveFile", jsonString);
        SaveOnline("CarGameSaveFile", jsonString);
    }

    public void LoadData()
    {

        //load from file
        string jsonString = Load("CarGameSaveFile");

        //load from server
      
        string jsonString2 = LoadOnline("CarGameSaveFile");

        if (jsonString != jsonString2)
        {
            Debug.LogError("Not the same!!!!");
        }

        var multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString2);
        var carHandler = FindObjectOfType<CarHandler>();
        var players = FindObjectsOfType<Mover>();

        if(multiplePlayers.players.Length <= 0)
        {
            Debug.Log("no save file exists yet");
        }
        else
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].name = multiplePlayers.players[i].Name;
                players[i].transform.position = multiplePlayers.players[i].Position;
                carHandler.SetNameTag(i, multiplePlayers.players[i].Name);
            }
        }
    }
    public void LoadData(int numOfNames)
    {

        //load from player prefs
        //string jsonString = PlayerPrefs.GetString("json");

        //load from file
        string jsonString = Load("CarGameSaveFile");

        //load from server        
        string jsonString2 = LoadOnline("CarGameSaveFile");

        if (jsonString != jsonString2)
        {
            Debug.LogError("Not the same!!!!");
        }

        var multiplePlayers = JsonUtility.FromJson<MultiplePlayers>(jsonString2);
        var nameTagManager = FindObjectOfType<NameSetter>();

        for (int i = 0; i < numOfNames; i++)
        {
            nameTagManager.SetNameField(i, multiplePlayers.players[i].Name);
        }
    }


    public void SaveToFile(string fileName, string jsonString)
    {
        // Open a file in write mode. This will create the file if it's missing.
        // It is assumed that the path already exists.
        using (var stream = File.OpenWrite(Application.persistentDataPath + "\\" + fileName))
        {
            // Truncate the file if it exists (we want to overwrite the file)
            stream.SetLength(0);

            // Convert the string into bytes. Assume that the character-encoding is UTF8.
            // Do you not know what encoding you have? Then you have UTF-8
            var bytes = Encoding.UTF8.GetBytes(jsonString);

            // Write the bytes to the hard-drive
            stream.Write(bytes, 0, bytes.Length);

            // The "using" statement will automatically close the stream after we leave
            // the scope - this is VERY important
        }
    }
    public void SaveOnline(string fileName, string saveData)
    {
        //url
        var request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + fileName);
        request.ContentType = "application/json";
        request.Method = "PUT";

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            streamWriter.Write(saveData);
        }

        var httpResponse = (HttpWebResponse)request.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
        }
    }


    public string Load(string fileName)
    {
        // Open a stream for the supplied file name as a text file
        using (var stream = File.OpenText(Application.persistentDataPath + "\\" + fileName))
        {
            // Read the entire file and return the result. This assumes that we've written the
            // file in UTF-8
            return stream.ReadToEnd();
        }
    }
    public string LoadOnline(string name)
    {
        var request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + name);
        var response = (HttpWebResponse)request.GetResponse();

        // Open a stream to the server so we can read the response data it sent back from our GET request
        using (var stream = response.GetResponseStream())
        {
            using (var reader = new StreamReader(stream))
            {
                // Read the entire body as a string
                var body = reader.ReadToEnd();

                return body;
            }
        }
    }

}

