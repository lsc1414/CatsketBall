using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizedString
{
	[SerializeField] private string stringID;
	public string StringID { get { return stringID; } }
	[SerializeField] private string translatedString;
	public string TranslatedString { get { return translatedString; } }
}
