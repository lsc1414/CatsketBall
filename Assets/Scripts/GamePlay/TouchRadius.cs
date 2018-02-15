using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRadius : MonoBehaviour, IAudioPlayable, IToggleable, ISettable<Ball>
{
	[SerializeField] private float forceMultiplier = 100f;
	private Rigidbody2D rb;

	[SerializeField] private float capSpeed = 10f;
	[SerializeField] private float minimumForce = 1f;

	private Vector3 startPosition;
	private AudioSource audioSource;

	private bool isActive;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnMouseDown()
	{
		if (gameObject.activeSelf == true)
		{
			Move();
		}
	}

	private void Move()
	{
		Vector3 touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		touchpos.z = 0;
		Vector3 force = transform.position - touchpos;
		if (force.magnitude < minimumForce) force = force.normalized * minimumForce;
		rb.AddForce(force * forceMultiplier);
		PlayAudio();
	}

	private void FixedUpdate()
	{
		transform.position = rb.transform.position - Vector3.forward; //ensuring onmousedown is called due to no z fightingalways
		if (rb.velocity.magnitude > capSpeed) rb.velocity = rb.velocity.normalized * capSpeed;
	}

	public void PlayAudio()
	{
		if (audioSource.enabled) { audioSource.Play(); }
	}

	public void Toggle(bool isActive)
	{
		gameObject.SetActive(isActive);
		rb.constraints = isActive ? RigidbodyConstraints2D.None : RigidbodyConstraints2D.FreezeAll;
	}

	public void Set(Ball sentT)
	{
		rb = sentT.GetComponent<Rigidbody2D>();
	}
}