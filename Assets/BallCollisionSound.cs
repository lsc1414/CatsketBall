using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionSound : MonoBehaviour 
{
	public Ball ball;

	private void OnCollisionEnter2D(Collision2D other)
	{
		ball.PlayHitSound();
	}
}
