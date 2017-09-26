using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBanner : MonoBehaviour {

	[SerializeField] private Text scoreText;
	[SerializeField] private Text timerText;
	[SerializeField] private Text highscoreText;
	public Text HighScoreText {get {return highscoreText;}}
	[SerializeField] private Text muteText;
	public Text MuteText {get {return muteText;}}
	[SerializeField] private GameObject homeButton;
	[SerializeField] private GameObject muteButton;
	[SerializeField] private GameObject restartButton;


	public void UpdateGameUI()
	{
		scoreText.text = "SCORE: " + GameManager.Score;
		timerText.text = "" + (int) GameManager.Timer;
		highscoreText.text = "HIGHSCORE: " + GameManager.HighScore;
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
}
