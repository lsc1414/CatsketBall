using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public int score = 0;
	public Text scoreText;
	public Text timerText;
	public Text menuText;

	public GameObject menu;

	public string endGameString = "Times up!";
	public string startGameString = "Click To Begin";

	public float startingTime = 60;

	private float timer = 60;
	public static bool gameHasStarted = false;

	public void Update()
	{
		if (gameHasStarted)
		{
			timer-= Time.deltaTime;
			if (timer <= 0f) EndGame();

			scoreText.text = "Score: " + score;
			timerText.text = "" + timer;
		}

		menuText.text = !gameHasStarted ? startGameString : endGameString;
	}

	public void Score()
	{
		score++;
	}

	public void EndGame()
	{
		gameHasStarted = false;
		timer = 0;
		menu.SetActive(true);
		//show the menu and restart everything
	}

	public void StartGame()
	{
		gameHasStarted=true;
		timer = startingTime;
		menu.SetActive(false);

	}
}