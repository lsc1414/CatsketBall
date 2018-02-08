using System.Collections;
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

	public void ProcessNewScore(int sentScore)
	{
		if (highScore < sentScore)
		{
			try
			{
				highScore = sentScore;
				PlayerPrefs.SetInt("highscore_" + levelInfo.levelName, sentScore);
				socialManager.ReportScore(sentScore, levelInfo.leaderBoardID);
				WriteHighScoreToUI();
			}
			catch (System.Exception e)
			{
				Debug.Log("Could not set new high score");
				e.ToString();
			}
		}
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
			tempHighScore = PlayerPrefs.GetInt("highscore_" + sentInfo.levelName, 0);
			Debug.Log("PlayerPref HighScore: " + tempHighScore);
			if (socialManager.GetIsAuthenticated())
			{
				int leaderBoardHighScore = socialManager.GetHighScoreFromLeaderBoard(sentInfo.leaderBoardID);
				Debug.Log("Leaderboard HighScore: " + leaderBoardHighScore);
				if (tempHighScore < leaderBoardHighScore)
				{
					PlayerPrefs.SetInt("highscore_" + sentInfo.levelName, leaderBoardHighScore);
					tempHighScore = leaderBoardHighScore;
				}
			}
		}
		catch (System.Exception e)
		{
			Debug.Log("Could not Get High Score");
			e.ToString();
		}
		return tempHighScore;
	}

}
