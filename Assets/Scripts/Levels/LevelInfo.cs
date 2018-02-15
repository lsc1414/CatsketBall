using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Catsketball/LevelInfo", order = 1)]
public class LevelInfo : ScriptableObject, IIdentifiable<string>, INameable, ISpriteProvider, ITimeIncrementProvider, IControllerProvider
{
	[Header("Level Info")] [SerializeField] private string levelName;
	public string Name { get { return levelName; } }
	[SerializeField] private int scoreToPass;
	public int ScoreToPass { get { return scoreToPass; } }
	[SerializeField] private string[] scoreStrings;
	[SerializeField] private string id;
	public string ID { get { return id; } }
	[SerializeField] private TimeIncrement[] timeIncrements;

	[Header("Level Resources")] [SerializeField] private Sprite stadiumSprite;
	public Sprite Sprite { get { return stadiumSprite; } }
	[SerializeField] private Ball ballPrefab;
	[SerializeField] private TouchRadius touchRadiusPrefab;

	public string GetScoreString()
	{
		return scoreStrings[UnityEngine.Random.Range(0, scoreStrings.Length)];
	}

	public float GetTimeIncrememnt(int sentScore)
	{
		for (int i = 0; i < timeIncrements.Length - 1; i++) //could probably replace all of this with some better linq minby command
		{
			if (timeIncrements[i].ScoreThreshold <= sentScore && timeIncrements[i + 1].ScoreThreshold > sentScore)
			{
				return timeIncrements[i].Increment;
			}
		}
		if (timeIncrements.Length > 0)
		{
			return timeIncrements[timeIncrements.Length - 1].Increment;
		}
		else return 0f;
	}

	public Ball GetBall()
	{
		return Instantiate(ballPrefab, new Vector3(0, 0, 0), ballPrefab.transform.rotation);
	}

	public TouchRadius GetTouchRadius()
	{
		return Instantiate(touchRadiusPrefab, new Vector3(0, 0, 0), touchRadiusPrefab.transform.rotation);
	}
}
