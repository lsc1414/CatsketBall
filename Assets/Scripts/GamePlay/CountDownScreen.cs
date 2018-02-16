using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownScreen : MonoBehaviour, IDependant<float>, IToggleable, IResetable
{
	[SerializeField] private Text text;

	public Func<float> GetVital { get; set; }

	public void Reset()
	{
		text.text = "";
	}

	public void Toggle(bool isActive)
	{
		gameObject.SetActive(isActive);
	}

	private void Update()
	{
		float time = GetVital();
		if (time > 0)
		{
			text.text = "" + (int)GetVital();
			return;
		}
		text.text = "TIME'S UP";
	}
}
