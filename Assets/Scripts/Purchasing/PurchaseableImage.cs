using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseableImage : MonoBehaviour
{

	[SerializeField] private string itemName;
	public string ItemName { get { return itemName; } }

	public void Display(bool isVisible)
	{
		gameObject.SetActive(isVisible);
	}

}
