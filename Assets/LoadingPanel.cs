using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadingPanel : MonoBehaviour
{
	private bool isManuallyStopped = false;
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
	}

	public void Suspend()
	{
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
