﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
	[SerializeField] private SocialManager socialManager;
	[SerializeField] private Text highScoreText;
	[SerializeField] private Text gameOverHighScoreText;
	private int highScore;
	private LevelInfo levelInfo;

	private void Awake()
	{
		socialManager.SetHighScoreAction = CheckHighScore;
	}

	public void ProcessNewScore(int sentScore)
	{
		if (highScore < sentScore)
		{
			try
			{
				highScore = sentScore;
				PlayerPrefs.SetInt("highscore_" + levelInfo.Name, sentScore);
				Debug.Log("HighScore: " + levelInfo.ID + ", " + highScore);
				WriteHighScoreToUI();
			}
			catch (System.Exception e)
			{
				Debug.Log("Could not set new high score");
				e.ToString();
			}
		}
		socialManager.ReportScore(sentScore, levelInfo.ID);
	}

	public void SetLevelInfo(LevelInfo sentLevelInfo)
	{
		levelInfo = sentLevelInfo;
		CheckHighScore();
	}

	public void CheckHighScore()
	{
		highScore = GetLevelHighScore(levelInfo);
		WriteHighScoreToUI();
	}

	private void WriteHighScoreToUI()
	{
		highScoreText.text = "HIGHSCORE: " + highScore;
		gameOverHighScoreText.text = "HIGHSCORE: " + highScore;
	}

	public int GetLevelHighScore(LevelInfo sentInfo)
	{
		int tempHighScore = 0;
		try
		{
			tempHighScore = PlayerPrefs.GetInt("highscore_" + sentInfo.Name, 0);
		}
		catch (System.Exception e)
		{
			e.ToString();
			Debug.Log("No player prefs score");
		}
		try
		{
			Debug.Log("PlayerPref HighScore: " + tempHighScore);
			int leaderBoardHighScore = socialManager.GetHighScoreFromLeaderBoard(sentInfo.ID);
			Debug.Log("Leaderboard HighScore: " + leaderBoardHighScore);
			if (tempHighScore < leaderBoardHighScore)
			{
				PlayerPrefs.SetInt("highscore_" + sentInfo.ID, leaderBoardHighScore);
				tempHighScore = leaderBoardHighScore;
			}
		}
		catch (System.Exception e)
		{
			e.ToString();
		}
		return tempHighScore;
	}

}
