using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Net : MonoBehaviour, ISettable<Ball>
{
	private Rigidbody2D ballRigidbody;

	[SerializeField] private Collider2D netCollider;

	private bool isGoalPossible = false;

	public EventHandler OnScore;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && other.transform.position.y > transform.position.y)
			isGoalPossible = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player" && other.transform.position.y < transform.position.y)
		{
			if (isGoalPossible)
			{
				if (OnScore != null)
				{
					OnScore(this, new EventArgs());
				}
			}
		}
		isGoalPossible = false;
	}

	private void Update()
	{
		netCollider.isTrigger = ballRigidbody.velocity.y <= 0;
	}

	public void Set(Ball sentT)
	{
		ballRigidbody = sentT.GetComponent<Rigidbody2D>();
	}
}
