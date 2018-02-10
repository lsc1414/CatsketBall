using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class PriceSetter : MonoBehaviour
{
	[SerializeField] private string buyString;
	[SerializeField] private string itemName;
	public string ProductID { get { return itemName; } }
	[SerializeField] private Button button;
	[SerializeField] private Text text;
	private bool isPurchased;

	public void SetText(bool sentIsPurchased = false, string priceString = "")
	{
		Debug.Log("Setting Price Text");
		if (sentIsPurchased == true)
		{
			isPurchased = true;
		}
		if (isPurchased == true)
		{
			Debug.Log("Product had receipt");
			text.text = "Purchased";
			button.interactable = false;
			return;
		}
		text.text = buyString + " " + priceString;
		button.interactable = true;
	}
}
