using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIButton : MonoBehaviour
{

	[Header("Parameters")]
	public LevelInfo thisLevel;
	public bool isUnlocked;

	[Header("UI")]
	public Image levelImage;
	public Text levelNameText;
	public Text statusText;
	public Text requiredScoreText;
	public Text currentHighScoreText;
	public Color unlockedColour;
	public Color lockedColour;

	public void Set(LevelInfo previousLevel)
	{
		levelImage.sprite = thisLevel.stadiumSprite;
		levelNameText.text = thisLevel.levelName;
		currentHighScoreText.text = "CURRENT HIGHSCORE: " + PlayerPrefs.GetInt("highscore_" + thisLevel.levelName);
		isUnlocked = true;
		if (previousLevel != null)
		{
			int previousLevelHighScore = PlayerPrefs.GetInt("highscore_" + previousLevel.levelName);
			if (previousLevelHighScore < previousLevel.ScoreToPass)
			{
				isUnlocked = false;
			}
		}
		//using isUnlocked instead of score check allows for unlocking levels in editor
		if (isUnlocked == false)
		{
			statusText.text = "LOCKED";
			levelImage.color = lockedColour;
			requiredScoreText.text = "SCORE " + previousLevel.ScoreToPass + " IN " + previousLevel.levelName + " TO UNLOCK";
			GetComponentInChildren<Button>().interactable = false;
			return;
		}
		statusText.text = "";
		levelImage.color = unlockedColour;
		GetComponentInChildren<Button>().interactable = true;
		requiredScoreText.text = "";
	}

	public void OnClick()
	{
		Debug.Log("Clicked");
		if (isUnlocked == true)
		{
			GameManager GM = GameObject.FindObjectOfType<GameManager>();
			GM.UpdateLevel(thisLevel);
		}
	}

}
