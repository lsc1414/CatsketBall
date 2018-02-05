using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class PriceSetter : MonoBehaviour
{
	[SerializeField] private string buyString;
	[SerializeField] private string itemName;
	[SerializeField] private Button button;
	[SerializeField] private Text text;

	public void SetPriceText(IStoreController sentController)
	{
		if (sentController == null) { return; }
		Product product = sentController.products.WithID(itemName);
		if (product == null)
		{
			text.text = "Error";
			button.interactable = false;
			return;
		}
		if (product.hasReceipt == true)
		{
			text.text = "Purchased";
			button.interactable = false;
			return;
		}
		text.text = buyString + " " + product.metadata.localizedPriceString;
		button.interactable = true;
	}
}
