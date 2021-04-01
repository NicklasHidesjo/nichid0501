using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
	[SerializeField] int score;
	[SerializeField] bool doubleScore;
	[SerializeField] bool tripleScore;

	public int Score 
	{ 
		get 
		{ 
			if (doubleScore) 
			{ 
				return score * 2; 
			}
			if(tripleScore)
			{
				return score * 3;
			}
			return score;
		} 
	}
}
