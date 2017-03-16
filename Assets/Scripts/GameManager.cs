using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class GameManager : MonoBehaviour 
{
	[System.Serializable]
	public class TimeIncrement
	{
		public int scoreThreshold;
		public float timeIncrement;
	}

	public delegate void GameEvent();
	public static bool gameHasStarted = false;
	public static bool timeIsUp = false;

	[Header("UI")]
	public Text scoreText;
	public Text timerText;
	public Text highscoreText;
	public Text muteText;
	public GameObject timeUpText;
	public GameObject splashScreen;

	[Header("Config")]
	public float startingTime = 30;
	public TimeIncrement[] timeIncrements;

	[Header("Events")]
	public UnityEvent OnStart;
	public UnityEvent OnTimeUp;
	private float timer = 30;
	private int score = 0;

	private void Awake()
	{
		if (OnStart == null) OnStart = new UnityEvent();
		if (OnTimeUp == null) OnTimeUp = new UnityEvent();
		EndGame();
		OnTimeUp.AddListener(ShowTimeUpUI);
	}

	public void Update()
	{
		if (gameHasStarted)
		{
			timer-= Time.deltaTime;
			if (timer <= 0f) OnTimeUp.Invoke();

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
		timer+= GetTimeIncrementReward();
		if (PlayerPrefs.GetInt("highscore") < score) PlayerPrefs.SetInt("highscore", score);
	}

	private float GetTimeIncrementReward()
	{
		Array.Sort(timeIncrements, (x,y) => x.scoreThreshold.CompareTo(y.scoreThreshold)); // make sure timeIncrements are ordered chronologically
		
		for (int i =0; i< timeIncrements.Length-1; i++) //could probably replace all of this with some better linq minby command
			if (timeIncrements[i].scoreThreshold <= score && timeIncrements[i+1].scoreThreshold > score) return timeIncrements[i].timeIncrement;

		if (timeIncrements.Length > 0)
			return timeIncrements[timeIncrements.Length-1].timeIncrement;

		else return 0f;
	}

	public void EndGame()
	{
		gameHasStarted = false;
		timer = 0;
		timeUpText.SetActive(false);
		splashScreen.SetActive(true);
		int highscore = PlayerPrefs.GetInt("highscore");
		if (highscore > 0) highscoreText.text = "HIGHSCORE: " + highscore;
		//show the splashScreen and restart everything
	}

	public void StartGame()
	{
		gameHasStarted=true;
		timeIsUp = false;
		timer = startingTime;
		splashScreen.SetActive(false);
		score = 0;

		OnStart.Invoke();
	}

	private void ShowTimeUpUI()
	{
		timeIsUp = true;
		timeUpText.SetActive(true);
	}
}