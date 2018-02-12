using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadingPanel : MonoBehaviour
{
	private bool isManuallyStopped = false;
	private int callCounts;
	public bool IsDisplayed { get; protected set; }

	void OnApplicationFocus(bool isFocused)
	{
		if (isFocused)
		{
			if (isManuallyStopped == false)
			{
				Resume();
			}
		}
	}

	public void SetManualStopping(bool sentValue)
	{
		isManuallyStopped = sentValue;
		callCounts++;
	}

	public void Suspend()
	{
		IsDisplayed = true;
		gameObject.SetActive(true);
		try { Handheld.StartActivityIndicator(); }
		catch (Exception e)
		{
			Debug.Log("Device not HandHeld, ");
			e.ToString();
		}
	}

	public void Resume()
	{
		callCounts--;
		if (callCounts < 0) { callCounts = 0; }
		if (callCounts == 0)
		{
			IsDisplayed = false;
			isManuallyStopped = false;
			try { Handheld.StopActivityIndicator(); }
			catch (Exception e)
			{
				Debug.Log("Device not HandHeld, ");
				e.ToString();
			}
			gameObject.SetActive(false);
		}
	}

}
