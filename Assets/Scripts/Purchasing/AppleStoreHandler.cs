using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using System.Runtime.InteropServices;

public class AppleStoreHandler : StoreHandler
{
	private AppleReceipt appleReceipt;

	public override string PopCornProductID { get { return "PopCorn"; } }
	public override string ThreeDeeGlassesID { get { return "3DGlasses"; } }

	private AppleReceipt GetReceiptData(ConfigurationBuilder sentBuilder)
	{
		try
		{
			var appleConfig = sentBuilder.Configure<IAppleConfiguration>();
			var receiptData = System.Convert.FromBase64String(appleConfig.appReceipt);
			return new AppleValidator(AppleTangle.Data()).Validate(receiptData);
		}
		catch (System.Exception e)
		{
			e.ToString();
			Debug.Log("Could not create apple receipt");
			return null;
		}
	}

	public override void RestoreReciepts(IExtensionProvider sentProvider)
	{
		var apple = sentProvider.GetExtension<IAppleExtensions>();
		apple.RestoreTransactions((result) =>
		{
			if (result)
			{
				try
				{
					ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
					appleReceipt = GetReceiptData(builder);
					foreach (AppleInAppPurchaseReceipt productReceipt in appleReceipt.inAppPurchaseReceipts)
					{
						PurchaseAction(productReceipt.productID);
					}
				}
				catch (System.Exception e)
				{
					e.ToString();
					Debug.Log("Faled to restore purchases");
				}
			}
			RestoreAction(result);
		});
	}

	public override void DisplayNativeMessage(string title, string message)
	{
		try
		{
			showNativeAlert(title, message);
		}
		catch (System.Exception e)
		{
			e.ToString();
			Debug.Log("Cannot display native alerts");
		}
	}

	[DllImport("__Internal")]
	extern static public void showNativeAlert(string title, string message);
}
