using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : UIScreen
{
	[SerializeField] private Text scoreText;
	public Text ScoreText { get { return scoreText; } }

	public override void Show()
	{
		scoreText.text = "YOUR SCORE: " + GameManager.Score;
		base.Show();
	}

}
