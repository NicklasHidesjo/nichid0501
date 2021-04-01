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

	[SerializeField] bool aimedDouble = false;
	[SerializeField] bool aimedTripple = false;
	[SerializeField] GameObject playerView = null;

	[Header("Buttons")]
	[SerializeField] Button throwButton = null;
	[SerializeField] Button endTurn = null;

	[Header("Aim Texts")]
	[SerializeField] Text aim = null;
	[SerializeField] Text aimedFor = null;
	[SerializeField] Text lastHit = null;

	[Header("Player Texts")]
	[SerializeField] Text playerName = null;
	[SerializeField] Text totalScore = null;
	[SerializeField] Text currentThrow = null;
	[SerializeField] Text roundTotal = null;
	[SerializeField] Text status = null;


	public bool AimedDouble { get { return aimedDouble; } set { aimedTripple = false; aimedDouble = !aimedDouble; UpdateAimedText(); } }
	public bool AimedTripple { get { return aimedTripple; } set { aimedDouble = false; aimedTripple = !aimedTripple; UpdateAimedText(); } }

	public int Rounds { get; private set; }
	public bool DartDouble { get; set; }

	DartBoard dartBoard;

	UserInfo user;

	GameInfo game;

	public void StartGame(GameInfo game, UserInfo user)
	{
		FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).ValueChanged += UpdateGame;

		this.game = game;

		maxPlayers = game.maxPlayers;

		startingScore = game.startingScore;

		dartBoard = new DartBoard(this);

		game.throws = new List<int>();

		Rounds = game.round;

		this.user = user;

		if(game.status == "Completed")
		{
			StartCoroutine(RemoveGame());
			return;
		}

		UpdateButtons();
		UpdateAimedText();
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

		aimedDouble = false;
		aimedTripple = false;

		if(game == null)
		{
			return;
		}

		if (game.status == "Completed")
		{
			StartCoroutine(RemoveGame());
			return;
		}

		UpdateText();
		UpdateAimedText();
		UpdateButtons();
		StartCoroutine(HandleEndOfGame());
	}

	private void UpdateText()
	{
		int throwNumber = Mathf.Clamp(game.throws.Count + 1, 1, 3);
		UserInfo info = game.players[game.currentPlayer];
		playerName.text = "Player throwing: " + info.name;
		totalScore.text = "Total Score: " + info.score.ToString();
		currentThrow.text = "Current Throw: " + throwNumber.ToString();
		roundTotal.text = "Round Total: " + GetRoundScore().ToString();
	}

	private void UpdateAimedText()
	{
		string text = "Aiming for: ";

		if (aimedDouble)
		{
			text += "Double " + aim.text;
		}
		else if (aimedTripple)
		{
			text += "Triple " + aim.text;
		}
		else
		{
			text += aim.text;
		}

		aimedFor.text = text;
	}

	private void UpdateButtons()
	{
		throwButton.interactable = false;
		endTurn.interactable = false;

		if (game.status != "full") { return; }
		if (user.playerIndex != game.currentPlayer) { return; }

		endTurn.interactable = true;
		if (game.throws.Count >= 3) { return; }
		throwButton.interactable = true;
	}

	private IEnumerator HandleEndOfGame()
	{
		if (game.winners.Count > 0)
		{
			playerView.SetActive(false);

			game.status = "Completed";
			user.activeGame = "";

			status.gameObject.SetActive(true);
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

	public void ThrowDart()
	{
		int aimed = 0;
		string attemptedAim = aim.text;
		try
		{
			aimed = int.Parse(attemptedAim);
		}
		catch (FormatException e)
		{
			Debug.LogWarning(e);
			aimed = -1;
		}

		if (!ValidThrow(aimed)) { return; }

		int hit = dartBoard.ThrowDart(aimed, aimedDouble, aimedTripple);
		game.throws.Add(hit);

		Debug.Log("Aimed for: " + aimed + " Hit: " + hit);
		lastHit.text = "Last hit: " + hit.ToString();
		if (game.throws.Count >= 3)
		{
			throwButton.interactable = false;
		}

		StartCoroutine(SaveGame());
	}

	private static bool ValidThrow(int aimed)
	{
		bool valid = false;

		if (aimed <= 20 && aimed > 0)
		{
			valid = true;
		}
		if (aimed == 50 || aimed == 25)
		{
			valid = true;
		}
		Debug.Log(valid);
		return valid;
	}

	public void EndTurn()
	{
		UpdateUser();
		NextPlayer();
	}

	private void UpdateUser()
	{
		user.score -= DecreaseScore();
		if(user.score == 0)
		{
			user.hasWon = true;
		}
	}

	public int DecreaseScore()
	{
		int newScore = user.score - GetRoundScore();

		if (newScore < 0)
		{
			return 0;
		}

		if (newScore == 1)
		{
			return 0;
		}

		if (newScore == 0 && !DartDouble)
		{
			return 0;
		}

		return GetRoundScore();
	}
	private int GetRoundScore()
	{
		int roundScore = 0;

		foreach (var hit in game.throws)
		{
			roundScore += hit;
		}

		return Mathf.Abs(roundScore);
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
		if(game.winners.Count > 0)
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
		status.gameObject.SetActive(true);
		yield return StartCoroutine(FirebaseManager.Instance.RemoveCompletedGames());
	}

	public void QuitGame()
	{
		FirebaseDatabase.DefaultInstance.GetReference("games/" + game.gameID).ValueChanged -= UpdateGame;
		FindObjectOfType<SceneLoader>().LoadScene(1);
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
