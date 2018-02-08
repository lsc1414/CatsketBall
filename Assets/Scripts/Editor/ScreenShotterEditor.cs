using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScreenShotter))]
public class ScreenShotterEditor : Editor
{

	public override void OnInspectorGUI()
	{
		ScreenShotter screenShotter = (ScreenShotter)target;
		base.OnInspectorGUI();
		if (GUILayout.Button("Take ScreenShot"))
		{
			screenShotter.TakeScreenshot();
		}
	}

}
