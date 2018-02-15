using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : UIScreen
{
	[SerializeField] private Text currentLevelText;
	public Text CurrentLevelText { get { return currentLevelText; } }
}
