using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour 
{
	public GameManager gameManager;
	public Rigidbody2D ballRigidbody;

	public Collider2D netCollider;

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player" && other.transform.position.y < transform.position.y) gameManager.Score();
	}

	private void Update()
	{
		netCollider.isTrigger = ballRigidbody.velocity.y <= 0;
	}
}
