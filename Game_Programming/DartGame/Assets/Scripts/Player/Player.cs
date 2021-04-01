using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public int DecreaseScore(int decrease, int currentScore, bool dartDouble)
	{
		int trueDecrease = Mathf.Abs(decrease);

		int newScore = currentScore - trueDecrease;

		if(newScore < 0)
		{
			return 0;
		}	

		if(newScore == 1)
		{
			return 0;
		}

		if(newScore == 0 && !dartDouble)
		{
			return 0;
		}

		return trueDecrease;
	}
}
