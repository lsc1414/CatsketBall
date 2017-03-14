using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	[System.Serializable]
	public class TimeIncrement
	{
		public int scoreThreshold;
		public float timeIncrement;
	}

	public delegate void GameEvent();
	public static event GameEvent OnStart, OnTimeUp;
	public static bool gameHasStarted = false;

	[Header("UI")]
	public Text scoreText;
	public Text timerText;
	public Text highscoreText;
	public Text muteText;
	public GameObject menu;

	[Header("Config")]
	public float startingTime = 30;
	public TimeIncrement[] timeIncrements;

	private float timer = 30;
	private int score = 0;

	private void Awake()
	{
		EndGame();
		OnTimeUp+=ShowTimeUpUI;
	}

	public void Update()
	{
		if (gameHasStarted)
		{
			timer-= Time.deltaTime;
			if (timer <= 0f) OnTimeUp();

			scoreText.text = "SCORE: " + score;
			timerText.text = "" + (int) timer;
		}
	}

	public void Mute(bool b)
	{
		if (b) muteText.text = "MUTE" ;
		else muteText.text = "UNMUTE";
	}

	public void Score()
	{
		score++;
		timer+= GetTimeIncrement();
		if (PlayerPrefs.GetInt("highscore") < score) PlayerPrefs.SetInt("highscore", score);
	}

	private float GetTimeIncrement()
	{
		//sort these ffs
		for (int i =0; i< timeIncrements.Length-1; i++)
			if (timeIncrements[i].scoreThreshold <= score && timeIncrements[i+1].scoreThreshold > score) return timeIncrements[i].timeIncrement;

		if (timeIncrements.Length > 0)
			return timeIncrements[timeIncrements.Length-1].timeIncrement;

		else return 0f;
	}

	public void EndGame()
	{
		gameHasStarted = false;
		timer = 0;
		menu.SetActive(true);
		int highscore = PlayerPrefs.GetInt("highscore");
		if (highscore > 0) highscoreText.text = "HIGHSCORE: " + highscore;
		//show the menu and restart everything
	}

	public void StartGame()
	{
		gameHasStarted=true;
		timer = startingTime;
		menu.SetActive(false);
		score = 0;

		OnStart();
	}

	private void ShowTimeUpUI()
	{
		EndGame();
	}
}