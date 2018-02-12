using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using UnityEngine.Purchasing.Security;
using UnityEngine.Purchasing.MiniJSON;

public class PurchaseHandler : MonoBehaviour, IStoreListener
{
	private static IStoreController m_StoreController;
	private static IExtensionProvider m_StoreExtensionProvider;

	[SerializeField] private ExtrasScreen extrasScreen;
	[SerializeField] private BackgroundImage backgroundImage;
	[SerializeField] private LoadingPanel loadingPanel;
#if UNITY_IOS
	private AppleReceipt appleReceipt;
#endif

	public static string popCornProductID = "PopCorn";
	public static string threeDeeGlassesID = "3DGlasses";

	private bool isPurchasing;
	private bool isRestoring;

	private void Start()
	{
		if (m_StoreController == null)
		{
			loadingPanel.Suspend();
			InitializePurchasing();
			backgroundImage.DeActivatePurchasedImages();
		}
	}

	public void RefreshPurchaseables()
	{
		Debug.Log("Refreshing Purchases");
		RestorePurchases();
		if (isRestoring) { return; }
		loadingPanel.Resume();
	}

	public void InitializePurchasing()
	{
		if (IsInitialized())
		{
			return;
		}
		ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
#if UNITY_IOS
		try
		{
			CreateReceiptData(builder);
		}
		catch (System.Exception e)
		{
			e.ToString();
			Debug.Log("build platform not recognised for receipts");
		}
#endif
		builder.AddProduct(popCornProductID, ProductType.NonConsumable);
		builder.AddProduct(threeDeeGlassesID, ProductType.NonConsumable);
		UnityPurchasing.Initialize(this, builder);
	}

#if UNITY_IOS
	private void CreateReceiptData(ConfigurationBuilder sentBuilder)
	{
		try
		{
			var appleConfig = sentBuilder.Configure<IAppleConfiguration>();
			var receiptData = System.Convert.FromBase64String(appleConfig.appReceipt);
			appleReceipt = new AppleValidator(AppleTangle.Data()).Validate(receiptData);
		}
		catch (System.Exception e)
		{
			e.ToString();
			Debug.Log("Could not create apple receipt");
		}
	}
#endif


	private bool IsInitialized()
	{
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}


	public void BuyProductID(string productId)
	{
		if (IsInitialized())
		{
			if (isPurchasing == false)
			{
				isPurchasing = true;
				Product product = m_StoreController.products.WithID(productId);
				if (product != null && product.availableToPurchase)
				{
					loadingPanel.Suspend();
					m_StoreController.InitiatePurchase(product);
				}
				return;
			}
		}
	}

	public void RestorePurchases()
	{
		if (isPurchasing == false)
		{
			for (int i = 0; i < m_StoreController.products.all.Length; i++)
			{
				Product product = m_StoreController.products.all[i];
				if (product.hasReceipt == true)
				{
					extrasScreen.SetPurchased(product.definition.storeSpecificId);
					backgroundImage.SetPurchased(product.definition.storeSpecificId);
				}
			}
#if UNITY_IOS || UNITY_IOS
			try
			{
				RestoreReciepts();
			}
			catch (System.Exception e)
			{
				e.ToString();
				Debug.Log("No RestoreReceipts function on this platform");
			}
#endif
		}
	}

#if UNITY_IOS
	private void RestoreReciepts()
	{
		try
		{
			ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			var appleConfig = builder.Configure<IAppleConfiguration>();
			var receiptData = System.Convert.FromBase64String(appleConfig.appReceipt);
			var receipt = new AppleValidator(AppleTangle.Data()).Validate(receiptData);
			foreach (AppleInAppPurchaseReceipt productReceipt in receipt.inAppPurchaseReceipts)
			{
				extrasScreen.SetPurchased(productReceipt.productID);
				backgroundImage.SetPurchased(productReceipt.productID);
			}
		}
		catch (System.Exception e)
		{
			e.ToString();
			Debug.Log("Faled to restore purchases");
		}
	}
#endif

	public void ManualRestorePurchases()
	{
		loadingPanel.Suspend();
		isRestoring = true;
		RestoreByPlatform();
	}

#if UNITY_IOS
	private void RestoreByPlatform()
	{
		var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
		apple.RestoreTransactions((result) =>
		{
			CompleteRestore(result);
		});
		return;
	}
#endif

#if UNITY_ANDROID
	private void RestoreByPlatform()
	{
			CompleteRestore(true);
	}
#endif

	private void CompleteRestore(bool result)
	{
		Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
		isRestoring = false;
		RefreshPurchaseables();
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		Debug.Log("Purchase Handler initialized");
		m_StoreController = controller;
		m_StoreExtensionProvider = extensions;
		for (int i = 0; i < controller.products.all.Length; i++)
		{
			Product product = controller.products.all[i];
			extrasScreen.SetProductPrices(product.definition.storeSpecificId, product.metadata.localizedPriceString);
		}
		RefreshPurchaseables();
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("Purchase Handler initialize failed");
		loadingPanel.Resume();
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		isPurchasing = false;
		if (isRestoring == false)
		{
			RefreshPurchaseables();
			Debug.Log("Purchase Successful");
		}
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		isPurchasing = false;
		if (isRestoring) { return; }
		RefreshPurchaseables();
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}
