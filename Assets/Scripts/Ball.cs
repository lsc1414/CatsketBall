using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour {

	[SerializeField] private Vector3 startPosition;
	private Rigidbody2D rb;
	public float touchRadiusScale;
	public UnityEvent OnFinalBounce;

	public void Reset()
	{
		transform.position = startPosition;
	}

	public void Start()
	{
		if (OnFinalBounce == null) OnFinalBounce = new UnityEvent();
		transform.position = startPosition;
		if (rb == null) rb = GetComponent<Rigidbody2D>();
		transform.rotation = new Quaternion();
		rb.velocity = new Vector2(0, 0);
		rb.angularVelocity = 0;
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
