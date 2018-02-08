using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotter : MonoBehaviour
{
	[SerializeField] private Resolution[] resolutions;
	[SerializeField] private string screenRes;
	[SerializeField] private string screenShotTitle;

	public void TakeScreenshot()
	{
		ScreenCapture.CaptureScreenshot(screenShotTitle + "_" + screenRes + ".png");
		/*for (int i = 0; i < resolutions.Length; i++)
		{
			ScreenCapture.CaptureScreenshot(screenShotTitle + "_" + resolutions[i].ResName + ".png");
		}*/
	}
}
