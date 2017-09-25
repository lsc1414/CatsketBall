using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

	[SerializeField] private Text scoreText;
	public Text ScoreText {get {return scoreText;}}
	[SerializeField] private Text highScoreText;
	public Text HighScoreText {get {return highScoreText;}}

	public void OnEnable()
	{
		if (GameManager.HighScore > 0) highScoreText.text = "HIGHSCORE: " + GameManager.HighScore;
		scoreText.text = "YOUR SCORE: " + GameManager.Score;
		highScoreText.text = "HIGHSCORE: " + GameManager.HighScore;
	}

}
