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
			highScore = sentScore;
			PlayerPrefs.SetInt("highscore_" + levelInfo.levelName, sentScore);
			socialManager.ReportScore(sentScore, levelInfo.leaderBoardID);
			WriteHighScoreToUI();
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
		int tempHighScore = PlayerPrefs.GetInt("highscore_" + sentInfo.levelName);
		if (socialManager.GetIsAuthenticated())
		{
			int leaderBoardHighScore = socialManager.GetHighScoreFromLeaderBoard(sentInfo.leaderBoardID);
			if (tempHighScore < leaderBoardHighScore)
			{
				PlayerPrefs.SetInt("highscore_" + sentInfo.levelName, leaderBoardHighScore);
				tempHighScore = leaderBoardHighScore;
			}
		}
		return tempHighScore;
	}

}
