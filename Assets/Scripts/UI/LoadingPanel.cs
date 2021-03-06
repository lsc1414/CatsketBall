﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadingPanel : MonoBehaviour
{
	private int callCounts;
	public bool IsDisplayed { get; protected set; }

	private void Awake()
	{
#if UNITY_IOS || UNITY_ANDROID
		try
		{
			SetActiviteIndicator();
		}
		catch (System.Exception e)
		{
			e.ToString();
			Debug.Log("Build platform loading indicator not recognised");
		}
#endif
	}

#if UNITY_IOS
	private void SetActiviteIndicator()
	{
		Handheld.SetActivityIndicatorStyle(UnityEngine.iOS.ActivityIndicatorStyle.White);
	}
#endif

#if UNITY_ANDROID
	private void SetActiviteIndicator()
	{
		Handheld.SetActivityIndicatorStyle(UnityEngine.AndroidActivityIndicatorStyle.Small);
	}
#endif

	void OnApplicationFocus(bool isFocused)
	{
		if (isFocused)
		{
			Resume();
			return;
		}
		Suspend();
	}

	public void Suspend()
	{
		callCounts++;
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
