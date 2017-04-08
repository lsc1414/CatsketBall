using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Catsketball/LevelInfo", order = 1)]
public class LevelInfo : ScriptableObject
{
	[System.Serializable]
	public class TimeIncrement
	{
		public int scoreThreshold;
		public float timeIncrement;
	}

	public float startingTime = 30;
	public TimeIncrement[] timeIncrements;

    //public Texture backgroundTexture;
    //public Texture ballTexture;
    //public Texture netTexture;
//
    //public Vector2 gravity; //y=-9.81
    //public float forceMultiplier = 300f;
    //public float capSpeed = 10f;
    //public float minimumForce = 2f;
    //public float bounceSetting & other physics

    //publig string levelname - tooltip is to that highscores are saved separately
}
