using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
using System;


// this will need to be changed into a monobehaviour? 
public class Game : MonoBehaviour
{
	// change as many things as possible to use the information in GameInfo and not local variables here.

	public int startingScore;
	public int maxPlayers;

	[Header("Buttons")]
	[SerializeField] Button endTurn;

	[Header("Game objects")]
	[SerializeField] GameObject options;
	[SerializeField] GameObject playerView;

	[Header("Player Texts")]
	[SerializeField] Text player1Name;
	[SerializeField] Text player1Score;
	[SerializeField] Text player2Name;
	[SerializeField] Text player2Score;
	[SerializeField] GameObject gameCompleted;
	[SerializeField] Text status;
	[SerializeField] GameObject bustWindow;

	public int Rounds { get; private set; }
	public bool DartDouble { get; set; }
	public bool CanThrow { get; private set; }
	public bool GamePaused { get; private set; }

	[SerializeField] GameObject[] DisplayDarts;

	bool busted;

	UserInfo user;

	GameInfo game;

	DartThrower thrower;
	HitShower hitDisplayer;

	public void StartGame(GameInfo game, UserInfo user)
	{
		FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).ValueChanged += UpdateGame;

		this.game = game;

		maxPlayers = game.maxPlayers;

		startingScore = game.startingScore;

		thrower = FindObjectOfType<DartThrower>();
		hitDisplayer = FindObjectOfType<HitShower>();

		game.throws = new List<int>();

		Rounds = game.round;

		this.user = user;

		if(game.status == "Completed")
		{
			StartCoroutine(RemoveGame());
			return;
		}

		UpdateButtons();
	}

	private void UpdateGame(object sender, ValueChangedEventArgs args)
	{
		if (this == null) { return; }
		if (args.DatabaseError != null)
		{
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
		string jsonData = args.Snapshot.GetRawJsonValue();
		game = JsonUtility.FromJson<GameInfo>(jsonData);

		if(game == null)
		{
			return;
		}

		if (game.status == "Completed")
		{
			StartCoroutine(RemoveGame());
			return;
		}
		UpdateDarts();
		UpdateText();
		UpdateButtons();
		StartCoroutine(HandleEndOfGame());
	}

	private void UpdateDarts()
	{
		foreach (var dart in DisplayDarts)
		{
			dart.transform.position = new Vector3(1, 0, 0);
		}

		if (user.playerIndex == game.currentPlayer) { return; }

		int index = 0;

		foreach (var position in game.dartPositions)
		{
			DisplayDarts[index].transform.position = position;
			index++;
		}
	}

	private void UpdateText()
	{
		if(game.status != "full"){ return; }

		UserInfo player1 = game.players[0];
		player1Name.text = "Player: " + player1.name;
		player1Score.text = "Score: " + player1.score.ToString();

		UserInfo player2 = game.players[1];
		player2Name.text = "Player: " + player2.name;
		player2Score.text = "Score: " + player2.score.ToString();
	}

	private void UpdateButtons()
	{
		endTurn.interactable = false;

		if (game.status != "full") { return; }
		if (user.playerIndex != game.currentPlayer) { return; }
		endTurn.interactable = true;
		if (game.throws.Count >= 3) { return; }
		if (game.throws.Count == 0)
		{
			user.previousScore = user.score;
		}
		CanThrow = true;
	}

	private IEnumerator HandleEndOfGame()
	{
		if (game.winners.Count > 0)
		{
			playerView.SetActive(false);

			game.status = "Completed";
			user.activeGame = "";

			gameCompleted.SetActive(true);
			FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).ValueChanged -= UpdateGame;
			yield return SaveGame();
		}
	}

	public void NextPlayer()
	{
		game.players[user.playerIndex] = user;

		game.currentPlayer = game.currentPlayer + 1;

		if (game.currentPlayer >= game.maxPlayers)
		{
			Rounds += 1;
			game.round = Rounds;
			game.currentPlayer = 0;
			CheckWinner();
		}
		game.throws = new List<int>();
		StartCoroutine(SaveGame());
	}

	public void CheckWinner()
	{
		game.winners = new List<UserInfo>();
		foreach (var player in game.players)
		{
			if (player.hasWon)
			{
				game.winners.Add(player);
			}
		}
	}

	public void ThrowDart(int hit, bool doubleHit, Vector3 position)
	{
		DartDouble = doubleHit;
		game.throws.Add(hit);
		user.score = DecreaseScore(hit);

		game.dartPositions.Add(position);

		if(busted)
		{
			CanThrow = false;
			bustWindow.SetActive(true);
			return;
		}

		if (game.throws.Count >= 3)
		{
			CanThrow = false;
		}

		game.players[game.currentPlayer].score = user.score;
		StartCoroutine(SaveGame());
	}
	public void EndTurn()
	{
		thrower.RemoveDarts();
		hitDisplayer.RemoveTexts();
		game.dartPositions.Clear();

		if(busted)
		{
			bustWindow.SetActive(false);
			user.score = user.previousScore;
			busted = false;
		}

		UpdateUser();
		NextPlayer();
	}

	private void UpdateUser()
	{
		if(user.score == 0)
		{
			user.hasWon = true;
		}
	}

	public int DecreaseScore(int hit)
	{
		int newScore = user.score - hit;

		if (newScore < 0)
		{
			busted = true;
		}

		if (newScore == 1)
		{
			busted = true;
		}

		if (newScore == 0 && !DartDouble)
		{
			busted = true;
		}

		return newScore;
	}

	private IEnumerator SaveGame()
	{
		yield return StartCoroutine(FirebaseManager.Instance.SaveData("users/" + user.ID, JsonUtility.ToJson(user)));
		yield return StartCoroutine(FirebaseManager.Instance.SaveData("games/" + game.gameID, JsonUtility.ToJson(game)));
	}

	public void LeaveGame()
	{
		StartCoroutine(HandleLeavingGame());
	}

	private IEnumerator HandleLeavingGame()
	{
		user.activeGame = "";
		FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).ValueChanged -= UpdateGame;
		if (game.status == "new")
		{
			game.status = "Completed";
			yield return StartCoroutine(RemoveGame());
		}
		else
		{
			game.status = "Completed";
			yield return StartCoroutine(SaveGame());
		}
		FindObjectOfType<SceneLoader>().LoadScene(1);
	}

	private IEnumerator RemoveGame()
	{
		FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).ValueChanged -= UpdateGame;
		playerView.SetActive(false);
		user.activeGame = "";
		yield return StartCoroutine(SaveGame());
		if (game.winners != null && game.winners.Count > 0)
		{
			if (game.winners.Count > 1)
			{
				status.text = "its a tie";
			}
			else
			{
				status.text = game.winners[0].name + " Has won!";
			}
		}
		else
		{
			status.text = "A player has left the game, the game is now over";
		}
		gameCompleted.SetActive(true);
		status.gameObject.SetActive(true);
		yield return StartCoroutine(FirebaseManager.Instance.RemoveCompletedGames());
	}

	public void QuitGame()
	{
		FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).ValueChanged -= UpdateGame;
		FindObjectOfType<SceneLoader>().LoadScene(1);
	}

	public void ToggleOptions()
	{
		thrower.IncreaseForce = false;
		options.SetActive(!options.activeSelf);
		GamePaused = options.activeSelf;
	}

	public void OnDestroy()
	{
		try
		{
			FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).ValueChanged -= UpdateGame;
		}
		catch
		{
			Debug.LogWarning("well we couldn't remove it.");
		}	
	}
}
