using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.SocialPlatforms;

public class SocialManager : MonoBehaviour
{
	private void Start()
	{
		Debug.Log("Social Manager Starting");
		Social.localUser.Authenticate(callback => { Debug.Log(callback ? "Login succeessfull." : "Login failed."); });
		/*if (Social.localUser.authenticated)
		{
			ILeaderboard basketBallLeaderBoard = Social.CreateLeaderboard();
			basketBallLeaderBoard.id = "BasketBall";
			Debug.Log("LeaderBoard Created: " + basketBallLeaderBoard.id);
		}*/
	}

	public void ShowLeaderBoards()
	{
		Social.ShowLeaderboardUI();
	}

	public void ReportScore(int score, string leaderBoardID)
	{
		if (Social.localUser.authenticated)
		{
			Debug.Log("Score reporting user authenticated, using leaderboard: " + leaderBoardID);
			Social.ReportScore((long)score, leaderBoardID, callback => { Debug.Log(callback ? "Score reported." : "Score report failed."); });
		}
	}
}
