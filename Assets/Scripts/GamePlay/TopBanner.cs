using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBanner : MonoBehaviour, IDependant<float>, ISettable<int>
{
	[SerializeField] private Text scoreText;
	[SerializeField] private Text timerText;
	[SerializeField] private Text muteText;
	public Text MuteText { get { return muteText; } }

	public Func<float> GetVital { get; set; }

	[SerializeField] private GameObject homeButton;
	[SerializeField] private GameObject muteButton;
	[SerializeField] private GameObject restartButton;


	public void UpdateGameUI()
	{
		int time = (int)GetVital();
		if (time < 0) { time = 0; }
		timerText.text = "" + time;
	}

	public void ToggleGamePlayUI(bool status)
	{
		if (status == false)
		{
			timerText.text = "";
			scoreText.text = "SCORE: 0";
		}
		restartButton.SetActive(status);
		homeButton.SetActive(status);
		scoreText.gameObject.SetActive(status);
		timerText.gameObject.SetActive(status);
	}

	public void Set(int sentT)
	{
		scoreText.text = "SCORE: " + sentT;
	}
}
