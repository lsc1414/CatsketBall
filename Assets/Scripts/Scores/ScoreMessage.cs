﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMessage : MonoBehaviour
{

	[SerializeField] private ScoreString encouragementString;
	[SerializeField] private ScoreString timeRewardString;
	private Vector3 originalPosition;

	public float timer = 2f;
	public float speed = 1f;

	public void SetText(string sentEncouragement, string sentTimeReward)
	{
		originalPosition = gameObject.transform.position;
		encouragementString.SetText(sentEncouragement);
		timeRewardString.SetText(sentTimeReward);
		gameObject.SetActive(true);
		StartCoroutine(WaitForReset());
	}

	private void FixedUpdate()
	{
		transform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
	}

	private IEnumerator WaitForReset()
	{
		yield return new WaitForSeconds(timer);
		Reset();
	}

	public void Reset()
	{
		gameObject.SetActive(false);
		transform.position = originalPosition;
	}


}
