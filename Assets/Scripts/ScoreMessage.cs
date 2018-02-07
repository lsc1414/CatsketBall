using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMessage : MonoBehaviour
{

	[SerializeField] private ScoreString encouragementString;
	[SerializeField] private ScoreString timeRewardString;

	public float timer = 2f;
	public float speed = 1f;

	public void SetText(string sentEncouragement, string sentTimeReward)
	{
		encouragementString.SetText(sentEncouragement);
		timeRewardString.SetText(sentTimeReward);
		Invoke("DelMe", timer);
	}

	private void FixedUpdate()
	{
		transform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
	}

	private void DelMe()
	{
		Destroy(this.gameObject);
	}
}
