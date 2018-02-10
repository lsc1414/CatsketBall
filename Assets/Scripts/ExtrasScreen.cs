using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class ExtrasScreen : UIScreen
{
	[SerializeField] private PriceSetter[] priceSetters;
	private IStoreController storeController;

	public void SetPurchased(string sentID)
	{
		for (int i = 0; i < priceSetters.Length; i++)
		{
			if (priceSetters[i].ProductID == sentID)
			{
				priceSetters[i].SetText(true);
			}
		}
	}

	public void SetProductPrices(string sentID, string sentPrice)
	{
		for (int i = 0; i < priceSetters.Length; i++)
		{
			if (priceSetters[i].ProductID == sentID)
			{
				priceSetters[i].SetText(false, sentPrice);
			}
		}
	}

	public string[] GetProductIDs()
	{
		string[] tempStrings = new string[priceSetters.Length];
		for (int i = 0; i < priceSetters.Length; i++)
		{
			tempStrings[i] = priceSetters[i].ProductID;
		}
		return tempStrings;
	}
}
