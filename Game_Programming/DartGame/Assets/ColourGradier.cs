using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourGradier : MonoBehaviour
{
	[SerializeField] Color32 startColour;
	[SerializeField] Color32 endColour;

	[SerializeField] Slider slider;
	[SerializeField] Image image;

	private void Start()
	{
		ChangeColour();
	}

	public void ChangeColour()
	{
		float value = (slider.value - slider.minValue) / (slider.maxValue - slider.minValue);
		image.color = Color32.Lerp(startColour, endColour, value);
	}

}
