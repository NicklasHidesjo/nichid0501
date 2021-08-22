using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayToggler : MonoBehaviour
{
	[SerializeField] GameObject HowToPlayMenu;

	public void ToogleHowToPlay()
	{
		HowToPlayMenu.SetActive(!HowToPlayMenu.activeSelf);
	}
}
