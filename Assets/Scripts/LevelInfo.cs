using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Catsketball/LevelInfo", order = 1)]
public class LevelInfo : ScriptableObject
{
	[Header("Level Info")]
    [Tooltip("Needs to be different for each level to ensure highscores dont overlap")]
    public string levelName;
    public float ballScale = 1f;
    public float netTranslateHeight = 0f;
    public float netWidthScale = 1f;

	[System.Serializable]
	public class TimeIncrement
	{
		public int scoreThreshold;
		public float timeIncrement;
	}

	[Header("Timers")]
	public float startingTime = 30;
	public TimeIncrement[] timeIncrements;

	[Header("Level Graphics")]
    public Sprite stadiumSprite;
    public Sprite ballSprite;
    public Sprite netSprite;

    [Header("Physics")]
    public Vector2 gravity;
    public float forceMultiplier = 300f;
    public float capSpeed = 10f;
    public float minimumForce = 2f;


    public void ApplySettings(GameManager GM)
    {
    	if (GM.ball == null) { Debug.LogError("Touch radius not assigned to gamemanager"); Debug.Break(); }

    	Physics2D.gravity = gravity;
    	//bounce settings etc

    	GM.ballTouchRadius.forceMultiplier = forceMultiplier;
    	GM.ballTouchRadius.capSpeed = capSpeed;
    	GM.ballTouchRadius.minimumForce = minimumForce;
    	GM.ball.gameObject.transform.localScale*=ballScale;

    	GM.stadiumRenderer.sprite = stadiumSprite;
    	GM.ballRenderer.sprite = ballSprite;
    	GM.netRenderer.sprite = netSprite;

    	GM.net.position+= new Vector3(0f, netTranslateHeight, 0f);

    	float newNetWidth = GM.net.localScale.x*netWidthScale;

    	GM.net.localScale = new Vector3( newNetWidth, GM.net.localScale.y, GM.net.localScale.z);
    }
}
