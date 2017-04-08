using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour 
{
	public GameManager gameManager;
	public Rigidbody2D ballRigidbody;

	public Collider2D netCollider;

	private bool isGoalPossible = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && other.transform.position.y > transform.position.y)
			isGoalPossible = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player" && other.transform.position.y < transform.position.y) 
		{
			if (isGoalPossible) gameManager.Score();
		}

		isGoalPossible=false;
	}

	private void Update()
	{
		netCollider.isTrigger = ballRigidbody.velocity.y <= 0;
	}

	public void AssignBall(GameObject ballObj)
	{
		ballRigidbody = ballObj.GetComponent<Rigidbody2D>();
	}
}
