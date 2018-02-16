using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class AppleMessageHandler : NativeMessageHandler
{
	public override void DisplayNativeMessage(string title, string message)
	{
		try
		{
			ShowNativeAlert(title, message);
		}
		catch (System.Exception e)
		{
			e.ToString();
			Debug.Log("Cannot display native alerts");
		}
	}

	[DllImport("__Internal")]
	extern static public void ShowNativeAlert(string title, string message);

}
