﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRadius : MonoBehaviour 
{
	public float forceMultiplier =100f;
	public Rigidbody2D RB;
	public Controller controller;
	public Ball ball;

	public float capSpeed = 10f;
	public float minimumForce = 1f;

	private Vector3 startPosition;

	private void OnMouseDown()
	{
		if (controller.enabled == true)
		{
			controller.Move();
		}
	}

	private void Update()
	{
		RB.constraints = !GameManager.gameHasStarted ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;
		transform.position=RB.transform.position - Vector3.forward; //ensuring onmousedown is called due to no z fightingalways
		if (RB.velocity.magnitude > capSpeed) RB.velocity = RB.velocity.normalized*capSpeed;
	}

}

