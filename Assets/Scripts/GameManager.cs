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
	private AudioSource[] audios;
	
	[Header("Level Info")]
	public LevelInfo levelInfo;
	public TouchRadius ballTouchRadius;
	public Ball ball;
	public Transform net;
	public PlayArea playArea;

	[Header("UI")]
	public Text scoreText;
	public Text timerText;
	public Text highscoreText;
	public Text muteText;
	public Text muteShadowText;
	public Text gameOverScoreText;
	public Text gameOverHighScoreText;
	private Text timeUpText;
	public Text currentLevelText;
	public GameObject timeUpTextObj;
	public GameObject splashScreen;
	public GameObject gameOverScreen;
	public GameObject levelSelectScreen;
	public ScoreString scoreStringPrefab;
	public ScoreString scoreString;
	public GameObject restartButton;
	public GameObject homeButton;
	public GameObject muteButton;

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
		UpdateLevel(levelInfo);

		if (OnStart == null) OnStart = new UnityEvent();
		if (OnTimeUp == null) OnTimeUp = new UnityEvent();
		if (OnScore == null) OnScore = new UnityEvent();

		timeUpText = timeUpTextObj.GetComponent<Text>();
		EndGame();
		splashScreen.SetActive(true);
		gameOverScreen.SetActive(false);
		levelSelectScreen.SetActive(false);
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
		audios = GameObject.FindObjectsOfType<AudioSource>();
		if (b)
		{
			muteText.text = "MUTE" ;
			muteShadowText.text = "MUTE";
			foreach (AudioSource audio in audios)
			{
				audio.enabled = true;
			}
		}
		else
		{
			muteText.text = "UNMUTE";
			muteShadowText.text = "UNMUTE";
			foreach (AudioSource audio in audios)
			{
				audio.enabled = false;
			}
		}
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
		scoreString = Instantiate(scoreStringPrefab, new Vector3 (0,0.2F,-2), Quaternion.identity) as ScoreString;
		scoreString.SetText(s);
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
		Debug.Log("Waiting to End");
		yield return new WaitForSeconds(3F);
		EndGame();
	}

	public void EndGame()
	{
		timeUpTextObj.SetActive(false);
		gameOverScreen.SetActive(true);
		//splashScreen.SetActive(true);
		int highscore = PlayerPrefs.GetInt("highscore_" + levelInfo.levelName);
		if (highscore > 0) highscoreText.text = "HIGHSCORE: " + highscore;
		gameOverScoreText.text = "YOUR SCORE: " + score;
		gameOverHighScoreText.text = "HIGHSCORE: " + highscore;
		StopAllCoroutines();
		ResetGameState();
		//show the splashScreen and restart everything
	}

	public void CancelGame()
	{
		ResetGameState();
		scoreText.text = "SCORE: 0";
		ReturnToSplashScreen();
	}

	private void ResetGameState()
	{
		Debug.Log("Game ended");
		gameHasStarted = false;
		timer = 0;
		score = 0;
		timerText.text = "";
		restartButton.SetActive(false);
		homeButton.SetActive(false);
		scoreText.gameObject.SetActive(false);
		timerText.gameObject.SetActive(false);
		if (scoreString != null) Destroy(scoreString.gameObject);
		timeUpText.gameObject.SetActive(false);
	}

	public void StartGame()
	{
		gameHasStarted=true;
		timeIsUp = false;
		timer = levelInfo.startingTime;
		splashScreen.SetActive(false);
		gameOverScreen.SetActive(false);
		HideLevelSelectScreen();
		restartButton.SetActive(true);
		homeButton.SetActive(true);
		scoreText.gameObject.SetActive(true);
		timerText.gameObject.SetActive(true);
		score = 0;
		playArea.SetResetPosition(ball.startPosition);

		if (OnStart == null) OnStart = new UnityEvent();
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

	public void ShowLevelSelectScreen()
	{
		levelSelectScreen.SetActive(true);
		levelSelectScreen.GetComponent<LevelSelectScreen>().Display();
	}

	public void HideLevelSelectScreen()
	{
		levelSelectScreen.SetActive(false);
	}

	public void ShowExtrasScreen()
	{
		
	}

	public void HideExtrasScreen()
	{
		
	}

	public void UpdateLevel(LevelInfo sentInfo)
	{
		levelInfo = sentInfo;
		currentLevelText.text = "CURRENT LEVEL: " + levelInfo.levelName;
		levelInfo.ApplySettings(this);
		levelSelectScreen.GetComponent<LevelSelectScreen>().currentLevelText.text = "CURRENT LEVEL: " + levelInfo.levelName;
	}

	public void ReturnToSplashScreen()
	{
		splashScreen.SetActive(true);
		levelSelectScreen.SetActive(false);
		gameOverScreen.SetActive(false);
	}
}