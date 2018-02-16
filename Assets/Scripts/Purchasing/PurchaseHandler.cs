using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseHandler : MonoBehaviour, IStoreListener
{
	private IStoreController storeController;
	private IExtensionProvider storeExtensionProvider;

	private StoreHandler storeHandler;

	[SerializeField] private ExtrasScreen extrasScreen;
	[SerializeField] private BackgroundImage backgroundImage;
	[SerializeField] private LoadingPanel loadingPanel;

	private bool isInitializing;

	private bool isPurchasing;

#if UNITY_IOS
	public StoreHandler GetStoreHandler() { return new AppleStoreHandler(); }
#endif

#if UNITY_ANDROID
	public StoreHandler GetStoreHandler(){return new AndroidStoreHandler();}
#endif

	private void Start()
	{
		if (storeController == null)
		{
			isInitializing = true;
			storeHandler = GetStoreHandler();
			storeHandler.PurchaseAction = SetPurchasedProduct;
			storeHandler.RestoreAction = FinalizeRestore;
			backgroundImage.DeActivatePurchasedImages();
			InitializePurchasing();
		}
	}

	public void InitializePurchasing()
	{
		if (GetIsInitialized()) { return; }
		ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		builder.AddProduct(storeHandler.PopCornProductID, ProductType.NonConsumable);
		builder.AddProduct(storeHandler.ThreeDeeGlassesID, ProductType.NonConsumable);
		UnityPurchasing.Initialize(this, builder);
	}

	private bool GetIsInitialized()
	{
		return storeController != null && storeExtensionProvider != null;
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		Debug.Log("Purchase Handler initialized");
		storeController = controller;
		storeExtensionProvider = extensions;
		for (int i = 0; i < controller.products.all.Length; i++)
		{
			Product product = controller.products.all[i];
			extrasScreen.SetProductPrices(product.definition.storeSpecificId, product.metadata.localizedPriceString);
		}
		RestorePurchases();
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("Purchase Handler initialize failed");
		isInitializing = false;
	}

	public void BuyProductID(string productId)
	{
		if (GetIsInitialized())
		{
			if (isPurchasing == false)
			{
				isPurchasing = true;
				Product product = storeController.products.WithID(productId);
				if (product != null && product.availableToPurchase)
				{
					loadingPanel.Suspend();
					storeController.InitiatePurchase(product);
				}
				return;
			}
		}
		storeHandler.DisplayNativeMessage("Error", "Could not load store.  Please try again later");
	}

	public void RestorePurchases()
	{
		loadingPanel.Suspend();
		if (GetIsInitialized())
		{
			for (int i = 0; i < storeController.products.all.Length; i++)
			{
				Product product = storeController.products.all[i];
				if (product.hasReceipt == true)
				{
					SetPurchasedProduct(product.definition.storeSpecificId);
				}
			}
			storeHandler.RestoreReciepts(storeExtensionProvider);
			return;
		}
		FinalizeRestore(false);
	}

	private void SetPurchasedProduct(string productID)
	{
		PlayerPrefs.SetInt("has" + productID, 1);
		extrasScreen.SetPurchased(productID);
		backgroundImage.SetPurchased(productID);
	}

	private void FinalizeRestore(bool result)
	{
		Debug.Log("Finalizing Restore: " + result);
		Product[] products = storeController.products.all;
		for (int i = 0; i < products.Length; i++)
		{
			try
			{
				if (PlayerPrefs.GetInt("has" + products[i].definition.id) == 1)
				{
					SetPurchasedProduct(products[i].definition.id);
				}
			}
			catch (System.Exception e)
			{
				e.ToString();
				SetPurchasedProduct(products[i].definition.id);
			}
		}
		isInitializing = false;
		loadingPanel.Resume();
		if (!result && !isInitializing)
		{
			storeHandler.DisplayNativeMessage("Error", "Could not restore purchases.  Please try again later");
		}
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		isPurchasing = false;
		loadingPanel.Resume();
		SetPurchasedProduct(args.purchasedProduct.definition.id);
		Debug.Log("Purchase Successful");
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		isPurchasing = false;
		loadingPanel.Resume();
		storeHandler.DisplayNativeMessage("Error", "Could not process purchase.  Please try again later");
		Debug.Log(string.Format("Purchase Failed: " + product.definition.storeSpecificId + ", " + failureReason));
	}
}
