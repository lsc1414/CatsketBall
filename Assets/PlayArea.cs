using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour {

	private Vector2 resetPosition;


	public void SetResetPosition(Vector2 pos)
	{
		resetPosition = pos;	
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("Fixing Ball Position");
		if (other.tag == "Player")
		{
			other.transform.position =  resetPosition;
		}
	}
}
