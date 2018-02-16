using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.SocialPlatforms.GameCenter;
#endif
using UnityEngine.SocialPlatforms;
using System.Linq;
using UnityEngine.Events;

public class SocialManager : MonoBehaviour
{
	[SerializeField] private LoadingPanel loadingPanel;

	public UnityEvent OnAuthenticate;
	private ILeaderboard leaderBoard;
	private bool isAuthenticating;

	public System.Action SetHighScoreAction;

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
		if (isAuthenticating)
		{
			StartCoroutine(WaitToDisplayLeaderBoard());
			return;
		}
		if (GetIsAuthenticated() == false)
		{
			AuthenticateUser(FinalizeLeaderBoardDisplay);
			return;
		}
		FinalizeLeaderBoardDisplay();
	}

	private void FinalizeLeaderBoardDisplay()
	{
		Social.ShowLeaderboardUI();
		loadingPanel.Resume();
	}

	private IEnumerator WaitToDisplayLeaderBoard()
	{
		while (isAuthenticating)
		{
			yield return null;
		}
		FinalizeLeaderBoardDisplay();
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

	public void AuthenticateUser(System.Action sentAction = null)
	{
		isAuthenticating = true;
		Social.localUser.Authenticate(isAuthenticated =>
		{
			Debug.Log(isAuthenticated ? "Login succeessfull." : "Login failed.");
			isAuthenticating = false;
			if (isAuthenticated)
			{
				OnAuthenticate.Invoke();
			}
			if (sentAction != null)
			{
				sentAction();
			}
			if (SetHighScoreAction != null)
			{
				SetHighScoreAction();
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
