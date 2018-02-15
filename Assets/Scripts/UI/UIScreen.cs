using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour, IToggleable
{

	protected virtual void Show()
	{
		gameObject.SetActive(true);
	}

	protected virtual void Hide()
	{
		gameObject.SetActive(false);
	}

	public void Toggle(bool isActive)
	{
		if (isActive)
		{
			Show();
			return;
		}
		Hide();
	}
}
