using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIButton : MonoBehaviour {

	[Header("Parameters")]
	public LevelInfo thisLevel;
	public LevelInfo previousLevel;
	public int requiredScore;
	public bool isUnlocked;


	[Header("UI")]
	public Image levelImage;
	public Text levelNameText;
	public Text statusText;
	public Text requiredScoreText;
	public Color unlockedColour;
	public Color lockedColour;

	private void Awake()
	{
		levelImage.sprite = thisLevel.stadiumSprite;
		levelNameText.text = thisLevel.levelName;
		if (isUnlocked)
		{
			statusText.text = "";
			levelImage.color = unlockedColour;
			requiredScoreText.text = "";
		}
		else
		{
			statusText.text = "LOCKED";
			levelImage.color = lockedColour;
			requiredScoreText.text = "SCORE " + requiredScore + " IN " + previousLevel.levelName + " TO UNLOCK";
		}
	}

}
