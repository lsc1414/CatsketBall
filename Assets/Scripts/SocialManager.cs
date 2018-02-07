using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.SocialPlatforms;
using System.Linq;
using UnityEngine.Events;

public class SocialManager : MonoBehaviour
{
	[SerializeField] private LoadingPanel loadingPanel;

	public UnityEvent OnAuthenticate;

	private void Awake()
	{
		if (OnAuthenticate == null) { OnAuthenticate = new UnityEvent(); }
	}
	private void Start()
	{
		Social.localUser.Authenticate(isAuthenticated =>
		{
			Debug.Log(isAuthenticated ? "Login succeessfull." : "Login failed.");
			if (isAuthenticated)
			{
				OnAuthenticate.Invoke();
			}
		});
	}

	public void ShowLeaderBoards()
	{
		Social.ShowLeaderboardUI();
	}

	public void ReportScore(int score, string leaderBoardID)
	{
		if (Social.localUser.authenticated)
		{
			Social.ReportScore((long)score, leaderBoardID, callback => { Debug.Log(callback ? "Score reported." : "Score report failed."); });
		}
	}

	public bool GetIsAuthenticated()
	{
		return Social.localUser.authenticated;
	}

	public int GetHighScoreFromLeaderBoard(string sentLeaderBoardID)
	{
		int tempHighScore = 0;
		if (GetIsAuthenticated())
		{
			try
			{
				Social.LoadScores(sentLeaderBoardID, scores =>
				{
					for (int i = 0; i < scores.Length; i++)
					{
						if ((int)scores[i].value > tempHighScore)
						{
							tempHighScore = (int)scores[i].value;
						}
					}
				});
			}
			catch (System.Exception e) { Debug.Log(e.ToString()); }
		}
		return tempHighScore;
	}
}
