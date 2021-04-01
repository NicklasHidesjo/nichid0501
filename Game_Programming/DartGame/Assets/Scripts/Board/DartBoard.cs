using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DartBoard
{
	int[] areas = {20,1,18,4,13,6,10,15,2,17,3,19,7,16,8,11,14,9,12,5};
	int outerBull = 25;
	int bull = 50;

	// the chances of hitting different areas;
	int missChance;
	int bullChance;
	int outerBullChance;
	int otherAreaHitChance;

	int doubleHitChance;
	int tripleHitChance;

	Game manager;

	public DartBoard(Game manager)
	{
		this.manager = manager;
	}

	public int ThrowDart(int aim, bool aimedForDouble, bool aimedForTriple)
	{
		manager.DartDouble = false;
		SetPercentagesForHit(aim, aimedForDouble, aimedForTriple);
		return HandleDartThrow(aim);
	}

	private void SetPercentagesForHit(int aim, bool aimedForDouble, bool aimedForTriple)
	{
		SetAimChances(5, 10, 15, 60);
		SetDoubleOrTripleChance(10, 10);

		if (aimedForDouble)
		{
			SetDoubleOrTripleChance(25, 5);
		}

		if (aimedForTriple)
		{
			SetDoubleOrTripleChance(5, 25);
		}

		if (aim == 25)
		{
			SetAimChances(2, 17, 51, 100);
			SetDoubleOrTripleChance(3, 12);
		}

		if (aim == 50)
		{
			SetAimChances(1, 16, 50, 100);
			SetDoubleOrTripleChance(2, 10);
		}
	}

	/// <summary>
	/// The different % chances for each outcome.
	/// to get proper % value the value needs to be the one before it + it's % chance.
	/// ( bull% = miss% + % for bull) 
	/// </summary>
	/// <param name="miss"></param>
	/// <param name="bull"></param>
	/// <param name="outerbull"></param>
	/// <param name="otherhit"></param>
	private void SetAimChances(int miss, int bull, int outerbull, int otherhit)
	{
		missChance = miss;
		bullChance = bull;
		outerBullChance = outerbull;
		otherAreaHitChance = otherhit;
	}
	private void SetDoubleOrTripleChance(int doubleChance, int tripleChance)
	{
		doubleHitChance = doubleChance;
		tripleHitChance = tripleChance;
	}


	private int HandleDartThrow(int aim)
	{
		int doubleHit = Random.Range(0, 100);
		int tripleHit = Random.Range(0, 100);

		int hit = GetHit(aim);

		if(hit == 50)
		{
			manager.DartDouble = true;
			return hit;
		}

		if(hit == 25)
		{
			return hit;
		}

		if (doubleHit < doubleHitChance)
		{
			manager.DartDouble = true;
			hit *=2;
		}

		if (tripleHit < tripleHitChance)
		{
			hit *= 3;
		}

		return hit;
	}

	private int GetHit(int aim)
	{
		int hitChance = Random.Range(0, 100);

		if (hitChance < missChance)
		{
			return 0;
		}

		if (hitChance < bullChance)
		{
			return bull;
		}

		if (hitChance < outerBullChance)
		{
			return outerBull;
		}

		if (hitChance < otherAreaHitChance)
		{
			return HandleOtherAreaHit(aim);
		}

		return aim;
	}

	private int HandleOtherAreaHit(int aim)
	{
		if(aim == 50 || aim == 25)
		{
			return GetOtherAreaHit();
		}
		return GetOtherAreaHit(aim);
	}

	private int GetOtherAreaHit()
	{
		return areas[Random.Range(0, areas.Length)];
	}

	private int GetOtherAreaHit(int aim)
	{
		int hitIndex = GetHitIndex(aim);
		return areas[hitIndex];
	}
	private int GetHitIndex(int aim)
	{
		int index = GetAimedIndex(aim);

		index = GetLeftOrRightOfAim(index);

		return index;
	}
	private int GetAimedIndex(int aim)
	{
		int index = 0;
		for (int i = 0; i < areas.Length; i++)
		{
			if (aim == areas[i])
			{
				index = i;
				break;
			}
		}

		return index;
	}
	private int GetLeftOrRightOfAim(int index)
	{
		int hitChance = Random.Range(0, 100);
		if (hitChance < 50)
		{
			index++;
			if (index >= areas.Length)
			{
				index = 0;
			}
		}
		else
		{
			index--;
			if (index < 0)
			{
				index = areas.Length - 1;
			}
		}

		return index;
	}
}
