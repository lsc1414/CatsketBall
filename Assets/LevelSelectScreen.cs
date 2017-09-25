using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectScreen : MonoBehaviour {

	[SerializeField] private LevelUIButton[] levelUIButtons;
	public LevelUIButton[] LevelUIButtons {get {return levelUIButtons;}}
	[SerializeField] private Text currentLevelText;
	public Text CurrentLevelText {get {return currentLevelText;}}

	public void Display()
	{
		levelUIButtons[0].isUnlocked = true;
		levelUIButtons[0].Set(null);
		LevelInfo level;
		for (int i = 1; i < levelUIButtons.Length; i++)
		{
			level = levelUIButtons[i].thisLevel;
			levelUIButtons[i].isUnlocked = false;
			if (PlayerPrefs.GetInt("highscore_" + level.levelName) >= level.ScoreToPass)
			{
				levelUIButtons[i].isUnlocked = true;
			}
			levelUIButtons[i].Set(levelUIButtons[i-1].thisLevel);
		}
	}
}
