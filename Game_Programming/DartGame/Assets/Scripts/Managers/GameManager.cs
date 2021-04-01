using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    UserInfo user;
	GameInfo game;
    string userID;                  
    FirebaseManager fbManager;      

    void Start()
    {
        userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        fbManager = FirebaseManager.Instance;
		game = new GameInfo();

        StartCoroutine(fbManager.LoadData("users/" + userID, LoadedUser));
    }

	private void LoadedUser(string jsonData)
	{
		user = JsonUtility.FromJson<UserInfo>(jsonData);
		CheckedActiveGame();
	}

    private void CheckedActiveGame()
    {
        if (user.activeGame == "" || user.activeGame == null)
        {
            StartCoroutine(fbManager.CheckForGame("games/", NewGameLoaded));
        }
        else
        {
            StartCoroutine(fbManager.LoadData("games/" + user.activeGame, GameLoaded));
        }
    }

    private void NewGameLoaded(string jsonData)
    {
        if (jsonData == "" || jsonData == null || jsonData == "{}")
		{
			CreateNewGame();
		}
		else
		{
			JoinNewGame(jsonData);
		}
	}

	private void CreateNewGame()
	{
		string key = FirebaseDatabase.DefaultInstance.RootReference.Child("games/").Push().Key;
		CreateGame(key);
		UpdateUser(game);
		SaveGame("games/" + key, "users/" + userID);
		GameLoaded(game);
	}
	private void CreateGame(string key)
	{
		game.players = new List<UserInfo>();
		game.players.Add(user);
		game.status = "new";
		game.gameID = key;
		game.maxPlayers = 2;
		game.startingScore = 301;
		game.round = 0;
		game.currentPlayer = 0;
		game.throws = new List<int>();
		game.winners = new List<UserInfo>();
	}

	private void JoinNewGame(string jsonData)
	{
		UpdateGame(jsonData);

		UpdateUser(game);

		SaveGame("games/" + game.gameID, "users/" + userID);

		GameLoaded(game);
	}

	private void UpdateGame(string jsonData)
	{
		game = JsonUtility.FromJson<GameInfo>(jsonData);
		game.players.Add(user);
		if (game.players.Count >= game.maxPlayers)
		{
			game.status = "full";
		}
	}
	private void UpdateUser(GameInfo game)
	{
		int playerNumber = Mathf.Clamp(game.players.Count - 1, 0, int.MaxValue);
		user.playerIndex = playerNumber;
        user.activeGame = game.gameID;
        user.score = 301;
		user.hasWon = false;
	}

	private void SaveGame(string path, string userPath)
	{
		StartCoroutine(fbManager.SaveData(userPath, JsonUtility.ToJson(user)));
		StartCoroutine(fbManager.SaveData(path, JsonUtility.ToJson(game)));
	}

	private void GameLoaded(string jsonData)
	{
		Debug.Log(jsonData);

		if (jsonData == null || jsonData == "")
		{
			user.activeGame = "";
			StartCoroutine(fbManager.SaveData("users/" + user.ID, JsonUtility.ToJson(user)));
			FindObjectOfType<SceneLoader>().LoadScene(1);
		}
		else
		{
			GameLoaded(JsonUtility.FromJson<GameInfo>(jsonData));
		}
	}
	private void GameLoaded(GameInfo game)
	{
		GetComponent<Game>().StartGame(game, user);
	}
}
