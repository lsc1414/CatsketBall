using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Catsketball/LevelInfo", order = 1)]
public class LevelInfo : ScriptableObject
{
	[Header("Level Info")]
	[Tooltip("Needs to be different for each level to ensure highscores dont overlap")]
	public string levelName;
	[SerializeField] private int scoreToPass;
	public int ScoreToPass { get { return scoreToPass; } }
	public float netTranslateHeight = 0f;
	public float netWidthScale = 1f;
	public string[] scoreStrings;
	public string leaderBoardID;

	[System.Serializable]
	public class TimeIncrement
	{
		public int scoreThreshold;
		public float timeIncrement;
	}

	[Header("Timers")]
	public float startingTime = 30;
	public TimeIncrement[] timeIncrements;

	[Header("Level Resources")]
	public Sprite stadiumSprite;
	public GameObject ballPrefab;
	public TouchRadius touchRadiusPrefab;
	public Sprite netSprite;


	public void ApplySettings(GameManager GM)
	{
		if (GM.ball == null) { Debug.LogError("Touch radius not assigned to gamemanager"); Debug.Break(); }

		GM.stadiumRenderer.sprite = stadiumSprite;
		GameObject oldBall = GM.ball.gameObject;
		GameObject oldTouchRadius = GM.touchRadius.gameObject;
		GameObject ballObj = Instantiate(ballPrefab, new Vector3(0, 0, 0), ballPrefab.transform.rotation);
		TouchRadius touchRadius = (TouchRadius)Instantiate(touchRadiusPrefab, new Vector3(0, 0, 0), touchRadiusPrefab.transform.rotation);
		Ball ball = ballObj.GetComponent<Ball>();
		touchRadius.ball = ball;
		ball.OnFinalBounce.AddListener(GM.OnFinalBounce);
		GM.OnStart.AddListener(ball.Start);
		GM.ball = ball;
		GM.touchRadius = touchRadius;
		touchRadius.AssignBall(ball);
		GM.net.GetComponentInChildren<Net>().AssignBall(ballObj);
		Destroy(oldBall);
		Destroy(oldTouchRadius);
		GM.netRenderer.sprite = netSprite;
	}

	public string GetScoreString()
	{
		return scoreStrings[Random.Range(0, scoreStrings.Length)];
	}
}
