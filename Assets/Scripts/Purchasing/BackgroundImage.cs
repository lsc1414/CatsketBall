using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class BackgroundImage : MonoBehaviour
{
	[SerializeField] private PurchaseableImage[] purchaseableImages;

	public void SetPurchased(string sentID)
	{
		for (int i = 0; i < purchaseableImages.Length; i++)
		{
			if (purchaseableImages[i].ItemName == sentID)
			{
				purchaseableImages[i].Display(true);
				break;
			}
		}
	}

	public void DeActivatePurchasedImages()
	{
		for (int i = 0; i < purchaseableImages.Length; i++)
		{
			purchaseableImages[i].Display(false);
		}
	}
}
