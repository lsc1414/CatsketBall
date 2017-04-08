using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class GameManager : MonoBehaviour 
{
	public delegate void GameEvent();
	public static bool gameHasStarted = false;
	public static bool countDownIsActive = false;
	public static bool timeIsUp = false;
	
	[Header("Level Info")]
	public LevelInfo levelInfo;
	public TouchRadius ballTouchRadius;
	public Ball ball;
	public Transform net;

	[Header("UI")]
	public Text scoreText;
	public Text timerText;
	public Text highscoreText;
	public Text muteText;
	public Text gameOverScoreText;
	public Text gameOverHighScoreText;
	private Text timeUpText;
	public GameObject timeUpTextObj;
	public GameObject splashScreen;
	public GameObject gameOverScreen;
	public ScoreString scoreStringPrefab;

	[Header("Level Renderers")]
	public SpriteRenderer stadiumRenderer;
	public SpriteRenderer ballRenderer;
	public SpriteRenderer netRenderer;

	[Header("Events")]
	public UnityEvent OnStart;
	public UnityEvent OnTimeUp;
	public UnityEvent OnScore;
	private float timer = 30;
	private int score = 0;

	private void Awake()
	{
		if (levelInfo==null) { Debug.LogError("No Levelinfo assigned to gamemanager - in project folder Create/Catsketball/Levelinfo"); Debug.Break(); }
		levelInfo.ApplySettings(this);

		if (OnStart == null) OnStart = new UnityEvent();
		if (OnTimeUp == null) OnTimeUp = new UnityEvent();
		if (OnScore == null) OnScore = new UnityEvent();

		timeUpText = timeUpTextObj.GetComponent<Text>();
		EndGame();
		gameOverScreen.SetActive(false);
		OnTimeUp.AddListener(ShowTimeUpUI);
	}

	public void Update()
	{
		if (gameHasStarted && timeIsUp == false)
		{
			timer-= Time.deltaTime;
			if (timer < 4 && timer > 0)
			{
				ShowCountDownUI();
				countDownIsActive = true;
			}
			else if (timer > 3)
			{
				if (timeUpTextObj.activeSelf)
				{
					countDownIsActive = false;
					timeUpTextObj.SetActive(false);
				}
			}
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
		OnScore.Invoke();
		MakeScoreString(levelInfo.GetScoreString());
		score++;
		timer+= GetTimeIncrementReward();
		if (PlayerPrefs.GetInt("highscore_" + levelInfo.levelName) < score) PlayerPrefs.SetInt("highscore_" + levelInfo.levelName, score);
	}

	private void MakeScoreString(string s)
	{
		ScoreString SS = Instantiate(scoreStringPrefab, net.position - Vector3.forward + Vector3.right, Quaternion.identity) as ScoreString;
		SS.SetText(s);
	}

	private float GetTimeIncrementReward()
	{
		Array.Sort(levelInfo.timeIncrements, (x,y) => x.scoreThreshold.CompareTo(y.scoreThreshold)); // make sure levelInfo.timeIncrements are ordered chronologically
		
		for (int i =0; i< levelInfo.timeIncrements.Length-1; i++) //could probably replace all of this with some better linq minby command
			if (levelInfo.timeIncrements[i].scoreThreshold <= score && levelInfo.timeIncrements[i+1].scoreThreshold > score) return levelInfo.timeIncrements[i].timeIncrement;

		if (levelInfo.timeIncrements.Length > 0)
			return levelInfo.timeIncrements[levelInfo.timeIncrements.Length-1].timeIncrement;

		else return 0f;
	}

	public void BeginWaitForGameEnd()
	{
		StartCoroutine("WaitToEndGame");
	}

	public IEnumerator WaitToEndGame()
	{
		yield return new WaitForSeconds(3F);
		EndGame();
	}

	public void EndGame()
	{
		gameHasStarted = false;
		timer = 0;
		timeUpTextObj.SetActive(false);
		gameOverScreen.SetActive(true);
		//splashScreen.SetActive(true);
		timerText.text = "";
		int highscore = PlayerPrefs.GetInt("highscore_" + levelInfo.levelName);
		if (highscore > 0) highscoreText.text = "HIGHSCORE: " + highscore;
		gameOverScoreText.text = "YOUR SCORE: " + score;
		gameOverHighScoreText.text = "HIGHSCORE: " + highscore;
		StopAllCoroutines();
		//show the splashScreen and restart everything
	}

	public void StartGame()
	{
		gameHasStarted=true;
		timeIsUp = false;
		timer = levelInfo.startingTime;
		splashScreen.SetActive(false);
		gameOverScreen.SetActive(false);
		score = 0;

		OnStart.Invoke();
	}

	private void ShowCountDownUI()
	{
		timeUpTextObj.SetActive(true);
		timeUpText.text = "" + (int) timer;
	}

	private void ShowTimeUpUI()
	{
		timeIsUp = true;
		timeUpText.text = "TIMES UP";
		timeUpTextObj.SetActive(true);
	}
}