using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectScreen : UIScreen, ISettable<string>
{
	[SerializeField] private HighScoreManager highScoreManager;
	[SerializeField] private LevelUIButton[] levelUIButtons;
	[SerializeField] private Text currentLevelText;

	public void Set(string sentT)
	{
		currentLevelText.text = sentT;
	}

	protected override void Show()
	{
		base.Show();
		levelUIButtons[0].Set(null, highScoreManager);
		for (int i = 1; i < levelUIButtons.Length; i++)
		{
			levelUIButtons[i].Set(levelUIButtons[i - 1].thisLevel, highScoreManager);
		}
	}
}
