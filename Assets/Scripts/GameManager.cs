using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;


public class GameManager : MonoBehaviour
{
	public delegate void GameEvent();
	public static bool gameHasStarted = false;
	public static bool countDownIsActive = false;
	public static bool timeIsUp = false;
	private static float timer;
	public static float Timer { get { return timer; } }
	private static int score;
	public static int Score { get { return score; } }
	[SerializeField] private SplashScreen splashScreen;
	[SerializeField] private GameOverScreen gameOverScreen;
	[SerializeField] private HighScoreManager highScoreManager;

	[Header("SubManagers")] [SerializeField] private UIManager uiManager;

	[Header("Level Info")]
	public LevelInfo levelInfo;
	public TouchRadius ballTouchRadius;
	public Ball ball;
	public Transform net;
	public PlayArea playArea;


	[Header("Level Renderers")]
	public SpriteRenderer stadiumRenderer;
	public SpriteRenderer ballRenderer;
	public SpriteRenderer netRenderer;

	[Header("Events")]
	public UnityEvent OnStart;
	public UnityEvent OnTimeUp;
	public UnityEvent OnScore;

	private void Awake()
	{
		if (levelInfo == null) { Debug.LogError("No Levelinfo assigned to gamemanager - in project folder Create/Catsketball/Levelinfo"); Debug.Break(); }
		UpdateLevel(levelInfo);

		if (OnStart == null) OnStart = new UnityEvent();
		if (OnTimeUp == null) OnTimeUp = new UnityEvent();
		if (OnScore == null) OnScore = new UnityEvent();
		ResetGameState();
		OnTimeUp.AddListener(EndTimer);
	}

	public void Update()
	{
		if (gameHasStarted)
		{
			uiManager.TopBanner.UpdateGameUI();
			if (timeIsUp == false)
			{
				timer -= Time.deltaTime;
				if (timer < 4 && timer > 0)
				{
					countDownIsActive = true;
					uiManager.ShowCountDownUI();
				}
				else if (timer > 3)
				{
					if (countDownIsActive)
					{
						countDownIsActive = false;
						uiManager.HideCountDownUI();
					}
				}
				if (timer <= 0f)
				{
					OnTimeUp.Invoke();
				}
			}
		}
	}

	public void IncreaseScore()
	{
		OnScore.Invoke();
		float timeIncrement = GetTimeIncrementReward();
		score++;
		string timeIncrementString = "+ " + timeIncrement.ToString() + " SECONDS";
		uiManager.MakeScoreString(levelInfo.GetScoreString(), timeIncrementString);
		timer += timeIncrement;
		highScoreManager.ProcessNewScore(score);
	}

	private float GetTimeIncrementReward()
	{
		Array.Sort(levelInfo.timeIncrements, (x, y) => x.scoreThreshold.CompareTo(y.scoreThreshold)); // make sure levelInfo.timeIncrements are ordered chronologically

		for (int i = 0; i < levelInfo.timeIncrements.Length - 1; i++) //could probably replace all of this with some better linq minby command
			if (levelInfo.timeIncrements[i].scoreThreshold <= score && levelInfo.timeIncrements[i + 1].scoreThreshold > score) return levelInfo.timeIncrements[i].timeIncrement;

		if (levelInfo.timeIncrements.Length > 0)
			return levelInfo.timeIncrements[levelInfo.timeIncrements.Length - 1].timeIncrement;

		else return 0f;
	}

	public void BeginWaitForGameEnd()
	{
		StartCoroutine(WaitToEndGame());
	}

	public IEnumerator WaitToEndGame()
	{
		Debug.Log("Waiting to End");
		yield return new WaitForSeconds(3F);
		EndGame();
	}

	public void EndGame()
	{
		gameOverScreen.Show();
		ball.gameObject.SetActive(false);
		StopAllCoroutines();
		ResetGameState();
	}

	public void CancelGame()
	{
		ResetGameState();
		splashScreen.Show();
	}

	private void ResetGameState()
	{
		Debug.Log("Game ended");
		gameHasStarted = false;
		timer = 0;
		score = 0;
		uiManager.ToggleGamePlayUI(false);
	}

	public void StartGame()
	{
		StopAllCoroutines();
		gameHasStarted = true;
		timeIsUp = false;
		timer = levelInfo.startingTime;
		splashScreen.Hide();
		uiManager.ToggleGamePlayUI(true);
		score = 0;
		playArea.SetResetPosition(ball.startPosition);
		if (OnStart == null) OnStart = new UnityEvent();
		ball.gameObject.SetActive(true);
		OnStart.Invoke();
	}

	private void EndTimer()
	{
		timeIsUp = true;
		uiManager.ShowTimeUpUI();
	}

	public void UpdateLevel(LevelInfo sentInfo)
	{
		levelInfo = sentInfo;
		levelInfo.ApplySettings(this);
		uiManager.UpdateCurrentLevelUI(levelInfo.levelName);
		highScoreManager.SetLevelInfo(sentInfo);
	}

}