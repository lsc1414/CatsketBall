using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectScreen : MonoBehaviour {

	[SerializeField] private LevelUIButton[] levelUIButtons;
	public LevelUIButton[] LevelUIButtons {get {return levelUIButtons;}}
	[SerializeField] private Text currentLevelText;
	public Text CurrentLevelText {get {return currentLevelText;}}

	public void OnEnable()
	{
		levelUIButtons[0].Set(null);
		for (int i = 1; i < levelUIButtons.Length; i++)
		{
			levelUIButtons[i].Set(levelUIButtons[i-1].thisLevel);
		}
	}
}
