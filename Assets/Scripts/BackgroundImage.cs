using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class BackgroundImage : MonoBehaviour
{
	[SerializeField] private PurchaseableImage[] purchaseableImages;

	public void SetPurchaseableImages(IStoreController sentController)
	{
		for (int i = 0; i < purchaseableImages.Length; i++)
		{
			if (sentController.products.WithID(purchaseableImages[i].ItemName).hasReceipt)
			{
				purchaseableImages[i].gameObject.SetActive(true);
				continue;
			}
			purchaseableImages[i].gameObject.SetActive(false);
		}
	}

	public void SetPurchased(string sentID)
	{
		for (int i = 0; i < purchaseableImages.Length; i++)
		{
			if (purchaseableImages[i].ItemName == sentID)
			{
				purchaseableImages[i].gameObject.SetActive(true);
			}
		}
	}

	public void DeActivatePurchasedImages()
	{
		for (int i = 0; i < purchaseableImages.Length; i++)
		{
			purchaseableImages[i].gameObject.SetActive(false);
		}
	}
}
