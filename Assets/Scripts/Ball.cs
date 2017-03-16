using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour {

	private Vector3 startPosition;
	public UnityEvent OnFinalBounce;

	public void Reset()
	{
		transform.position = startPosition;
	}

	private void Start()
	{
		if (OnFinalBounce == null) OnFinalBounce = new UnityEvent();
		startPosition = transform.position;
	}

	public void PlayHitSound()
	{
		AudioSource AS = GetComponent<AudioSource>();
		if(AS.enabled) AS.Play();
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (GameManager.timeIsUp)
		{
			if (collision.gameObject.tag == "Floor")
			{
				OnFinalBounce.Invoke();
			}
		}
	}
}
