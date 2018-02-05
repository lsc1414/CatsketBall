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
	public ScoreString scoreStringPrefab;
	public ScoreString scoreString;
	private Text timeUpText;

	private void Awake()
	{
		//timeUpText = timeUpTextObj.GetComponent<Text>();
	}

	public void ToggleGamePlayUI(bool status)
	{
		if (status == false)
		{
			if (scoreString != null) Destroy(scoreString.gameObject);
			timeUpTextObj.gameObject.SetActive(false);
		}
		topBanner.ToggleGamePlayUI(status);
	}

	public void ChangeButtonText(string sentString, Text sentText)
	{
		sentText.text = sentString;
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

	public void MakeScoreString(string s)
	{
		scoreString = Instantiate(scoreStringPrefab, new Vector3(0, 0.2F, -2), Quaternion.identity) as ScoreString;
		scoreString.SetText(s);
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