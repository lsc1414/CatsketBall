using System;
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

	public static string popCornProductID = "PopCorn";
	public static string threeDeeGlassesID = "3DGlasses";

	void Awake()
	{
		if (m_StoreController == null)
		{
			InitializePurchasing();
			extrasScreen.SetStoreController(m_StoreController);
			RefreshPurchaseables();
		}
	}

	private void RefreshPurchaseables()
	{
		RestorePurchases();
		extrasScreen.SetPurchaseButtons();
		backgroundImage.SetPurchaseableImages(m_StoreController);
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
			Product product = m_StoreController.products.WithID(productId);
			if (product != null && product.availableToPurchase)
			{
				m_StoreController.InitiatePurchase(product);
			}
			RefreshPurchaseables();
			return;
		}
	}

	public void RestorePurchases()
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

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		m_StoreController = controller;
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		RefreshPurchaseables();
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}
