using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	public float power =1f;
	public Rigidbody2D RB;

	public float capSpeed = 10f;

	private void OnMouseDown()
	{
		Vector3 touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		touchpos.z=0;
		
		RB.AddForce((transform.position-touchpos)*power); // ensure everything is z=0
		Debug.Log("Ball clicked");

	}

	private void Update()
	{
		RB.constraints = !GameManager.gameHasStarted ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;
		transform.position=RB.transform.position;
		if (RB.velocity.magnitude > capSpeed) RB.velocity = RB.velocity.normalized*capSpeed;
	}

}

