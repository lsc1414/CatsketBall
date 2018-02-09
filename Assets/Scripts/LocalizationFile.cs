﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizationFile
{
	[SerializeField] private LocalizedString[] localizedStrings;
	public LocalizedString[] LocalizedStrings { get { return localizedStrings; } }
}
