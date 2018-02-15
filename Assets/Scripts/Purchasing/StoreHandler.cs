using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using UnityEngine.Purchasing.Security;
using UnityEngine.Purchasing.MiniJSON;
using System.Runtime.InteropServices;

public abstract class StoreHandler
{
	public abstract string PopCornProductID { get; }
	public abstract string ThreeDeeGlassesID { get; }

	public System.Action<string> PurchaseAction { get; set; }
	public System.Action<bool> RestoreAction { get; set; }

	public abstract void RestoreReciepts(IExtensionProvider sentProvider);
	public abstract void DisplayNativeMessage(string title, string message);
}
