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
		highScore = PlayerPrefs.GetInt("highscore_" + levelInfo.levelName);
		if (socialManager.GetIsAuthenticated())
		{
			int tempHighScore = socialManager.GetHighScoreFromLeaderBoard(levelInfo.leaderBoardID);
			if (highScore < tempHighScore)
			{
				highScore = tempHighScore;
			}
		}
		WriteHighScoreToUI();
	}

	private void WriteHighScoreToUI()
	{
		highScoreText.text = "HIGHSCORE: " + highScore;
		gameOverHighScoreText.text = "HIGHSCORE: " + highScore;
	}

}
