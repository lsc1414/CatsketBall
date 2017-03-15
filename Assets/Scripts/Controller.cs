using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour{

	[SerializeField] private TouchRadius TR;

	public void Move()
	{
		Vector3 touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		touchpos.z=0;

		Transform TRTrans = TR.transform;
		Vector3 force = TRTrans.position-touchpos;
		if (force.magnitude < TR.minimumForce) force = force.normalized*TR.minimumForce;

		TR.RB.AddForce(force*TR.forceMultiplier); // ensure everything is z=0
		TR.ball.PlayHitSound();
	}
}
