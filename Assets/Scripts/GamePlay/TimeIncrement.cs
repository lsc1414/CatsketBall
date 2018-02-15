using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimeIncrement
{
	[SerializeField] private int scoreThreshold;
	public int ScoreThreshold { get { return scoreThreshold; } }
	[SerializeField] private float increment;
	public float Increment { get { return increment; } }
}
