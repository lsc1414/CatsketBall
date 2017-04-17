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
    public float netTranslateHeight = 0f;
    public float netWidthScale = 1f;
    public string[] scoreStrings;

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
    public GameObject ball;
    public Sprite netSprite;


    public void ApplySettings(GameManager GM)
    {
    	if (GM.ball == null) { Debug.LogError("Touch radius not assigned to gamemanager"); Debug.Break(); }

    	GM.stadiumRenderer.sprite = stadiumSprite;
		GameObject oldBall = GM.ball.gameObject;
		GameObject ballObj = Instantiate(ball, new Vector3 (0, 0, 0), ball.transform.rotation);
		GM.ballTouchRadius.ball = ballObj.GetComponent<Ball>();
		ballObj.GetComponent<Ball>().OnFinalBounce.AddListener(GM.BeginWaitForGameEnd);
		GM.ball = ballObj.GetComponent<Ball>();
		GM.ballTouchRadius.AssignBall();
		GM.net.GetComponentInChildren<Net>().AssignBall(ballObj);
		Destroy(oldBall);

    	GM.netRenderer.sprite = netSprite;

    	GM.net.position+= new Vector3(0f, netTranslateHeight, 0f);

    	float newNetWidth = GM.net.localScale.x*netWidthScale;

    	GM.net.localScale = new Vector3( newNetWidth, GM.net.localScale.y, GM.net.localScale.z);
    }

    public string GetScoreString()
    {
    	return scoreStrings[Random.Range(0, scoreStrings.Length)];
    }
}
