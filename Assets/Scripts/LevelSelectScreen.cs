using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectScreen : UIScreen
{
	[SerializeField] private HighScoreManager highScoreManager;
	[SerializeField] private LevelUIButton[] levelUIButtons;
	public LevelUIButton[] LevelUIButtons { get { return levelUIButtons; } }
	[SerializeField] private Text currentLevelText;
	public Text CurrentLevelText { get { return currentLevelText; } }

	public override void Show()
	{
		base.Show();
		levelUIButtons[0].Set(null, highScoreManager);
		for (int i = 1; i < levelUIButtons.Length; i++)
		{
			levelUIButtons[i].Set(levelUIButtons[i - 1].thisLevel, highScoreManager);
		}
	}
}
