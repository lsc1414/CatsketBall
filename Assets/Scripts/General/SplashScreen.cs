using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : UIScreen, ISettable<string>
{
	[SerializeField] private Text currentLevelText;

	public void Set(string sentT)
	{
		currentLevelText.text = sentT;
	}
}
