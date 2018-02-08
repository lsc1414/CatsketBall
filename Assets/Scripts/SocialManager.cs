﻿using System.Collections;
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
	private ILeaderboard leaderBoard;

	private void Awake()
	{
		if (OnAuthenticate == null) { OnAuthenticate = new UnityEvent(); }
	}
	private void Start()
	{
		AuthenticateUser();
	}

	public void ShowLeaderBoards()
	{
		loadingPanel.Suspend();
		if (GetIsAuthenticated() == false)
		{
			AuthenticateUser();
			return;
		}
		loadingPanel.Suspend();
		OnAuthenticate.Invoke();
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

	public void AuthenticateUser()
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

	public int GetHighScoreFromLeaderBoard(string sentLeaderBoardID)
	{
		Debug.Log("Getting HighScore: " + sentLeaderBoardID);
		int tempHighScore = 0;
		if (GetIsAuthenticated())
		{
			try
			{
				/*leaderBoard = Social.CreateLeaderboard();
				leaderBoard.id = sentLeaderBoardID;
				Debug.Log("Leaderboard: " + leaderBoard.ToString());
				leaderBoard.SetUserFilter(new string[] { Social.localUser.id });
				leaderBoard.LoadScores(scores =>
				{
					if (scores)
					{
						tempHighScore = (int)leaderBoard.localUserScore.value;
					}
				});*/
				Social.LoadScores(sentLeaderBoardID, scores =>
				{
					if (scores != null)
					{
						foreach (IScore score in scores)
						{
							if (score.userID == Social.localUser.id)
							{
								if ((int)score.value > tempHighScore)
								{
									tempHighScore = (int)score.value;
								}
							}
						}
					}
				});
			}
			catch (System.Exception e) { Debug.Log(e.ToString()); }
		}
		return tempHighScore;
	}
}
