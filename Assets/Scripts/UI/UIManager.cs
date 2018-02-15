using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	[Header("UIPanels")]
	[SerializeField]
	private TopBanner topBanner;
	public TopBanner TopBanner { get { return topBanner; } }
	[SerializeField] private SplashScreen splashScreen;
	[SerializeField] private LevelSelectScreen levelSelectScreen;
	[SerializeField] private ExtrasScreen extrasScreen;

	[Header("GamePlay")]
	public GameObject timeUpTextObj;
	[SerializeField] private ScoreMessage[] scoreMessages;
	public ScoreMessage scoreMessage;
	private Text timeUpText;

	private void Awake()
	{
		//timeUpText = timeUpTextObj.GetComponent<Text>();
	}

	public void ToggleGamePlayUI(bool status)
	{
		if (status == false)
		{
			for (int i = 0; i < scoreMessages.Length; i++)
			{
				scoreMessages[i].Reset();
			}
			timeUpTextObj.gameObject.SetActive(false);
		}
		topBanner.ToggleGamePlayUI(status);
	}

	public void ShowCountDownUI()
	{
		timeUpTextObj.SetActive(true);
		timeUpText = timeUpTextObj.GetComponent<Text>();
		timeUpText.text = "" + (int)GameManager.Timer;
	}

	public void HideCountDownUI()
	{
		timeUpTextObj.SetActive(false);
	}

	public void MakeScoreString(string scoreText, string timeRewardText)
	{
		for (int i = 0; i < scoreMessages.Length; i++)
		{
			if (scoreMessages[i].gameObject.activeSelf == false)
			{
				scoreMessage = scoreMessages[i];
				scoreMessage.SetText(scoreText, timeRewardText);
				break;
			}
		}
	}

	public void ShowTimeUpUI()
	{
		timeUpText.text = "TIMES UP";
		timeUpTextObj.SetActive(true);
	}

	public void UpdateCurrentLevelUI(string levelName)
	{
		splashScreen.CurrentLevelText.text = "CURRENT LEVEL: " + levelName;
		levelSelectScreen.CurrentLevelText.text = "CURRENT LEVEL: " + levelName;
	}
}