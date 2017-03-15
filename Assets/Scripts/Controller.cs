using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour{

	[SerializeField] private Ball ball;

	public void Move()
	{
		Vector3 touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		touchpos.z=0;

		Transform ballTrans = ball.transform;
		Vector3 force = ballTrans.position-touchpos;
		if (force.magnitude < ball.minimumForce) force = force.normalized*ball.minimumForce;

		ball.RB.AddForce(force*ball.forceMultiplier); // ensure everything is z=0
		ball.PlayHitSound();
	}
}
