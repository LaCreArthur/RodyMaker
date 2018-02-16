using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BtnSlider : MonoBehaviour {

	public Text sliderValue;
	public Slider s;
	public void OnChange() {
		sliderValue.text = Math.Round(s.value,1).ToString();
	}
}
