using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class PurchaseHandler : MonoBehaviour, IStoreListener
{
	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

	[SerializeField] private ExtrasScreen extrasScreen;
	[SerializeField] private BackgroundImage backgroundImage;
	[SerializeField] private LoadingPanel LoadingPanel;

	public static string popCornProductID = "PopCorn";
	public static string threeDeeGlassesID = "3DGlasses";

	private bool isPurchasing;

	private void Start()
	{
		if (m_StoreController == null)
		{
			InitializePurchasing();
			Handheld.SetActivityIndicatorStyle(UnityEngine.iOS.ActivityIndicatorStyle.White);
			extrasScreen.SetStoreController(m_StoreController);
			RefreshPurchaseables();
		}
	}

	public void RefreshPurchaseables()
	{
		Debug.Log("Refreshing Purchases");
		RestorePurchases();
		extrasScreen.SetPurchaseButtons();
		backgroundImage.SetPurchaseableImages(m_StoreController);
		LoadingPanel.Resume();
	}

	public void InitializePurchasing()
	{
		if (IsInitialized())
		{
			return;
		}
		ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		builder.AddProduct(popCornProductID, ProductType.NonConsumable);
		builder.AddProduct(threeDeeGlassesID, ProductType.NonConsumable);
		UnityPurchasing.Initialize(this, builder);
	}


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
					LoadingPanel.SetManualStopping(true);
					LoadingPanel.Suspend();
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
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
			{
				var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
				apple.RestoreTransactions((result) =>
				{
					Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
				});
			}
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		Debug.Log("Purchase Handler initialized");
		m_StoreController = controller;
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("Purchase Handler initialize failed");
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		FinishPurchase();
		Debug.Log("Purchase Successful");
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		FinishPurchase();
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

	public void FinishPurchase()
	{
		RefreshPurchaseables();
		isPurchasing = false;
	}
}
