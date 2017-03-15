using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	private Vector3 startPosition;

	private void Start()
	{
		startPosition = transform.position;
		GameManager.OnStart+= Reset;
	}

	public void Reset()
	{
		transform.position = startPosition;
	}

	public void PlayHitSound()
	{
		AudioSource AS = GetComponent<AudioSource>();
		if(AS.enabled) AS.Play();
	}
}
