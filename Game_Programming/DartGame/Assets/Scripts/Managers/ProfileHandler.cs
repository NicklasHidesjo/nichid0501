using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ProfileHandler : MonoBehaviour
{
    UserInfo user;
    string userID;                 
    FirebaseManager fbManager;

    [SerializeField] InputField playerName = null;
    [SerializeField] Text activeGame = null;


    void Start()
    {
        userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        fbManager = FirebaseManager.Instance;

        StartCoroutine(fbManager.LoadData("users/" + userID, LoadedUser));
    }

    private void LoadedUser(string jsonData)
    {
        if (jsonData == null || jsonData == "")
		{
			CreateUser();
        }
		else
        {
            user = JsonUtility.FromJson<UserInfo>(jsonData);
        }
        UpdateTexts();
    }

	private void CreateUser()
	{
		user = new UserInfo();
		user.activeGame = "";
		user.ID = userID;
		user.name = "pungspark";
		user.score = 301;
		user.hasWon = false;

        StartCoroutine(fbManager.SaveData("users/" + userID, JsonUtility.ToJson(user)));
    }

	private void UpdateTexts()
    {
        playerName.text = user.name;
        activeGame.text = user.activeGame;
    }

    public void Play()
	{
        user.name = playerName.text;
        StartCoroutine(fbManager.SaveData("users/" + userID, JsonUtility.ToJson(user)));
        FindObjectOfType<SceneLoader>().LoadScene(2);
    }

    public void LogOut()
	{
        FirebaseAuth.DefaultInstance.SignOut();
        FindObjectOfType<SceneLoader>().LoadScene(0);
	}
}
