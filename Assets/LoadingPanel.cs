using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadingPanel : MonoBehaviour
{
	public void Suspend()
	{
		gameObject.SetActive(true);
		try { Handheld.StartActivityIndicator(); }
		catch (Exception e)
		{
			Debug.Log("Device not HandHeld, " + e.ToString());
		}
	}

	public void Resume()
	{
		try { Handheld.StopActivityIndicator(); }
		catch (Exception e)
		{
			Debug.Log("Device not HandHeld, " + e.ToString());
		}
		gameObject.SetActive(false);
	}

}
