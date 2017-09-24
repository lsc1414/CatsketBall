using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectScreen : MonoBehaviour {

	public LevelUIButton[] levelUIButtons;
	public Text currentLevelText;

	public void Display()
	{
		for (int i = 0; i < levelUIButtons.Length; i++)
		{
			if (i == 0)
			{
				levelUIButtons[i].isUnlocked = true;
				levelUIButtons[i].Set();
			}
			else
			{
				levelUIButtons[i].isUnlocked = false;
				if (PlayerPrefs.GetInt("highscore_" + levelUIButtons[i].previousLevel.levelName) >= levelUIButtons[i].requiredScore)
				{
					levelUIButtons[i].isUnlocked = true;
				}
				levelUIButtons[i].Set();
			}
		}
	}
}
