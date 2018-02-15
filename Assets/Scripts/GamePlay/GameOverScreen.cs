using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : UIScreen, ISettable<int>
{
	[SerializeField] private Text scoreText;
	public Text ScoreText { get { return scoreText; } }

	public void Set(int sentT)
	{
		scoreText.text = "YOUR SCORE: " + sentT;
	}
}
