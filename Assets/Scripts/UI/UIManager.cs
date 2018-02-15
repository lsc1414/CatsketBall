using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	[Header("UIPanels")]
	[SerializeField]
	private TopBanner topBanner;
	public TopBanner TopBanner { get { return topBanner; } }
	[SerializeField] private SplashScreen splashScreen;
	[SerializeField] private LevelSelectScreen levelSelectScreen;
	[SerializeField] private ExtrasScreen extrasScreen;

	[Header("GamePlay")]
	[SerializeField]
	private ScoreMessage[] scoreMessages;
	public ScoreMessage scoreMessage;

	private void Awake()
	{
		//timeUpText = timeUpTextObj.GetComponent<Text>();
	}
}