using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resolution
{
	[SerializeField] private string resName;
	public string ResName { get { return resName; } }
	[SerializeField] private int width;
	public int Width { get { return width; } }
	[SerializeField] private int height;
	public int Height { get { return height; } }
}
