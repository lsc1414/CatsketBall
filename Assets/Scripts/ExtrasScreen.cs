using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class ExtrasScreen : UIScreen
{
	[SerializeField] private PriceSetter[] priceSetters;
	private IStoreController storeController;

	public void SetStoreController(IStoreController sentController)
	{
		storeController = sentController;
	}

	public override void Show()
	{
		base.Show();
		SetPurchaseButtons();
	}

	public void SetPurchaseButtons()
	{
		for (int i = 0; i < priceSetters.Length; i++)
		{
			priceSetters[i].SetPriceText(storeController);
		}
	}

}
