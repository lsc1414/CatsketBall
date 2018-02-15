using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
	/*private static LocalizedLanguage language;
	[SerializeField] private LocalizedLanguage[] languages;
	private Dictionary<SystemLanguage, LocalizedLanguage> languageDictionary;
	private LocalizedStringHolder localizedStringHolder;
	private static Dictionary<string, string> stringDictionary;

	public void Awake()
	{
		languageDictionary = new Dictionary<SystemLanguage, LocalizedLanguage>();
		for (int i = 0; i < languages.Length; i++)
		{
			languageDictionary.Add(languages[i].Language, languages[i]);
		}
		language = languageDictionary[SystemLanguage.English];
		languageDictionary.TryGetValue(Application.systemLanguage, out language);
		SetLocalizedStrings();
		stringDictionary = new Dictionary<string, string>();
		for (int i = 0; i < localizedStringHolder.LocalizedStrings.Length; i++)
		{
			stringDictionary.Add(localizedStringHolder.LocalizedStrings[i].StringID, localizedStringHolder.LocalizedStrings[i].TranslatedString);
		}
	}

	public void SetLocalizedStrings()
	{
		string path = language.FilePath;
		string text;
		using (StreamReader sr = new StreamReader(path))
		{
			text = sr.ReadToEnd();
		}
		localizedStringHolder = JsonUtility.FromJson<LocalizedStringHolder>(text);
	}


	public static string GetLocalizedString(string sentStringID)
	{
		return stringDictionary[sentStringID];
	}*/

}
