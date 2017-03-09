using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	public float forceMultiplier =100f;
	public Rigidbody2D RB;

	public float capSpeed = 10f;
	public float minimumForce = 1f;

	private Vector3 startPosition;

	private void OnMouseDown()
	{
		Vector3 touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		touchpos.z=0;

		Vector3 force = transform.position-touchpos;
		if (force.magnitude < minimumForce) force = force.normalized*minimumForce;
		
		RB.AddForce(force*forceMultiplier); // ensure everything is z=0
		PlayHitSound();
	}

	public void Reset()
	{
		RB.transform.position = startPosition;
	}

	private void Start()
	{
		startPosition = RB.transform.position;
		GameManager.OnStart+=Reset;
	}

	public void PlayHitSound()
	{
		AudioSource AS = GetComponent<AudioSource>();
		if(AS.enabled) AS.Play();
	}

	private void Update()
	{
		RB.constraints = !GameManager.gameHasStarted ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;
		transform.position=RB.transform.position - Vector3.forward; //ensuring onmousedown is called due to no z fightingalways
		if (RB.velocity.magnitude > capSpeed) RB.velocity = RB.velocity.normalized*capSpeed;
	}

}

