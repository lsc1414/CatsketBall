using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour, ISettable<Vector2>
{
	private Vector2 resetPosition;

	public void Set(Vector2 sentT)
	{
		resetPosition = sentT;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("Fixing Ball Position");
		if (other.tag == "Player")
		{
			other.transform.position = resetPosition;
		}
	}
}
