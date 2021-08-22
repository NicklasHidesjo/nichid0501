using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserInfo
{
	public string name;
	public string ID;
	public int score;
	public int previousScore;
	public string activeGame;
	public bool hasWon; // remove this once we are done with the UserForGame stuff
	public int playerIndex;
}

[Serializable]
public class GameInfo
{
	public int round;
	public int currentPlayer;
	public int maxPlayers;
	public int startingScore;

	public string gameID;
	public string status;

	public List<UserInfo> players;
	public List<UserInfo> winners;
	public List<int> throws;
	public List<Vector3> dartPositions;
}
