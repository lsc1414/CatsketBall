using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;


public class GameManager : MonoBehaviour, IToggleable, ISettable<LevelInfo>
{
	[SerializeField] private float startingTime;
	private bool gameHasStarted = false;
	private bool countDownIsActive = false;
	private bool timeIsUp = false;
	private float timer;
	private int score;
	[SerializeField] private SplashScreen splashScreen;
	[SerializeField] private GameOverScreen gameOverScreen;
	[SerializeField] private HighScoreManager highScoreManager;

	[Header("Level Info")] [SerializeField] private LevelInfo levelInfo;
	[SerializeField] private Net net;
	[SerializeField] private TouchRadius touchRadius;
	[SerializeField] private Ball ball;
	[SerializeField] private PlayArea playArea;


	[Header("Level Renderers")] [SerializeField] private SpriteRenderer stadiumRenderer;
	[SerializeField] private SpriteRenderer ballRenderer;
	[SerializeField] private SpriteRenderer netRenderer;

	[Header("UI")] [SerializeField] private CountDownScreen countDownScreen;
	[SerializeField] private TopBanner topBanner;
	[SerializeField] private ScoreMessage[] scoreMessages;
	[SerializeField] private LevelSelectScreen levelSelectScreen;

	private void Awake()
	{
		if (levelInfo == null) { Debug.LogError("No Levelinfo assigned to gamemanager - in project folder Create/Catsketball/Levelinfo"); Debug.Break(); }
		net.OnScore += new EventHandler(delegate (object sentObject, EventArgs e)
		{
			IncreaseScore();
		});
		playArea.Set(new Vector3(0, 0, 0));
		topBanner.GetVital = GetTime;
		Set(levelInfo);
		highScoreManager.CheckHighScore();
		countDownScreen.GetVital = GetTime;
		ResetGameState();
	}

	private void Update()
	{
		if (gameHasStarted)
		{
			if (timeIsUp == false)
			{
				timer -= Time.deltaTime;
				topBanner.UpdateGameUI();
				if (timer < 4 && timer > 0)
				{
					countDownIsActive = true;
					countDownScreen.Toggle(true);
				}
				else if (timer > 3)
				{
					if (countDownIsActive)
					{
						countDownIsActive = false;
						countDownScreen.Toggle(false);
					}
				}
				if (timeIsUp == false)
				{
					if (timer <= 0f)
					{
						timeIsUp = true;
						BeginWaitForGameEnd(6F);
					}
				}
			}
		}
	}

	private void ToggleGamePlayUI(bool status)
	{
		if (status == false)
		{
			for (int i = 0; i < scoreMessages.Length; i++)
			{
				scoreMessages[i].Reset();
			}
			countDownScreen.Toggle(false);
		}
		topBanner.ToggleGamePlayUI(status);
	}

	private float GetTime()
	{
		return timer;
	}

	private void IncreaseScore()
	{
		float timeIncrement = levelInfo.GetTimeIncrememnt(score);
		score++;
		string timeIncrementString = "+ " + timeIncrement.ToString() + " SECONDS";
		ShowScoreString(levelInfo.GetScoreString(), timeIncrementString);
		if (timeIsUp == false)
		{
			timer += timeIncrement;
		}
		topBanner.Set(score);
		highScoreManager.ProcessNewScore(score);
	}

	private void ShowScoreString(string scoreText, string timeRewardText)
	{
		for (int i = 0; i < scoreMessages.Length; i++)
		{
			if (scoreMessages[i].gameObject.activeSelf == false)
			{
				ScoreMessage scoreMessage = scoreMessages[i];
				scoreMessage.SetText(scoreText, timeRewardText);
				break;
			}
		}
	}

	private void BeginWaitForGameEnd(float sentTime)
	{
		StartCoroutine(WaitToEndGame(sentTime));
	}

	private IEnumerator WaitToEndGame(float sentTime)
	{
		Debug.Log("Waiting to End");
		yield return new WaitForSeconds(sentTime);
		EndGame();
	}

	private void EndGame()
	{
		highScoreManager.ProcessNewScore(score);
		gameOverScreen.Set(score);
		gameOverScreen.Toggle(true);
		ball.gameObject.SetActive(false);
		StopAllCoroutines();
		ResetGameState();
	}

	private void CancelGame()
	{
		ResetGameState();
		splashScreen.Toggle(true);
	}

	private void ResetGameState()
	{
		Debug.Log("Game ended");
		gameHasStarted = false;
		timeIsUp = false;
		timer = startingTime;
		score = 0;
		ToggleGamePlayUI(false);
		ball.Toggle(false);
	}

	private void StartGame()
	{
		StopAllCoroutines();
		ResetGameState();
		splashScreen.Toggle(false);
		countDownScreen.Toggle(false);
		topBanner.Set(score);
		ToggleGamePlayUI(true);
		ball.Toggle(true);
		ball.Reset();
		gameHasStarted = true;
	}

	public void Set(LevelInfo sentInfo)
	{
		levelInfo = sentInfo;
		highScoreManager.SetLevelInfo(sentInfo);
		stadiumRenderer.sprite = sentInfo.Sprite;
		Destroy(ball.gameObject);
		Destroy(touchRadius.gameObject);
		ball = sentInfo.GetBall();
		touchRadius = sentInfo.GetTouchRadius();
		ball.OnFinalBounce += new EventHandler(delegate (object sentObject, EventArgs e)
		{
			BeginWaitForGameEnd(3F);
		});
		net.Set(ball);
		touchRadius.Set(ball);
		splashScreen.Set("CURRENT LEVEL: " + sentInfo.Name);
		levelSelectScreen.Set("CURRENT LEVEL: " + sentInfo.Name);
	}

	public void Toggle(bool isActive)
	{
		if (isActive)
		{
			StartGame();
			return;
		}
		CancelGame();
	}
}