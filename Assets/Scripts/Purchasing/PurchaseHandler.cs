using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseHandler : MonoBehaviour, IStoreListener
{
    private IStoreController storeController;
    private IExtensionProvider storeExtensionProvider;

    private StoreHandler storeHandler;
    private NativeMessageHandler messageHandler;

    [SerializeField] private ExtrasScreen extrasScreen;
    [SerializeField] private BackgroundImage backgroundImage;
    [SerializeField] private LoadingPanel loadingPanel;

    private bool isInitializing;
    private bool isPurchasing;

#if UNITY_IOS
	public StoreHandler GetStoreHandler() { return new AppleStoreHandler(); }
	public NativeMessageHandler GetMessageHandler() { return new AppleMessageHandler(); }
#endif

#if UNITY_ANDROID
    public StoreHandler GetStoreHandler() { return new AndroidStoreHandler(); }
    public NativeMessageHandler GetMessageHandler() { return new AndroidMessageHandler(); }
#endif

    private void Start()
    {
        if (storeController == null)
        {
            isInitializing = true;
            storeHandler = GetStoreHandler();
            messageHandler = GetMessageHandler();
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
        FinalizeProcess();
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
        if (!isInitializing) { messageHandler.DisplayNativeMessage("Error", "Could not load store.  Please try again later"); }
    }

    public void RestorePurchases()
    {
        if (!isInitializing)
        {
            loadingPanel.Suspend();
        }
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
        if (!result)
        {
            ReStorePurchaseFromPlayerPrefs(storeHandler.PopCornProductID);
            ReStorePurchaseFromPlayerPrefs(storeHandler.ThreeDeeGlassesID);
            messageHandler.DisplayNativeMessage("Error", "Could not load store.  Please try again later");
        }
        FinalizeProcess();
    }

    private void ReStorePurchaseFromPlayerPrefs(string sentID)
    {
        try
        {
            if (PlayerPrefs.GetInt("has" + sentID) == 1)
            {
                SetPurchasedProduct(sentID);
            }
        }
        catch (System.Exception e) { Debug.Log("Purchase not stored in player prefs: " + sentID + ". " + e.ToString()); }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (isPurchasing) { FinalizeProcess(); }
        SetPurchasedProduct(args.purchasedProduct.definition.id);
        Debug.Log("Purchase Successful");
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        if (isPurchasing) { FinalizeProcess(); }
        if (isInitializing) { messageHandler.DisplayNativeMessage("Error", "Could not process purchase.  Please try again later"); }
        Debug.Log(string.Format("Purchase Failed: " + product.definition.storeSpecificId + ", " + failureReason));
    }

    private void FinalizeProcess()
    {
        loadingPanel.Resume();
        isInitializing = false;
        isPurchasing = false;
    }
}
