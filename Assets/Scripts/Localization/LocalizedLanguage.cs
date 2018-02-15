using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizedLanguage
{
	[SerializeField] private string filePath;
	public string FilePath { get { return filePath; } }
	[SerializeField] private SystemLanguage language;
	public SystemLanguage Language { get { return language; } }
}
