using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizedStringWriter : MonoBehaviour
{
	[SerializeField] private LocalizedStringHolder localizedStringHolder;
	/*public void WriteToJson(tList)
	{
		string jText = JsonUtility.ToJson(tList);
		Debug.Log(tList);
		using (FileStream fs = new FileStream(path, FileMode.Create))
		{
			using (StreamWriter sw = new StreamWriter(fs))
			{
				sw.Write(jText);
			}
		}
	}*/
}
