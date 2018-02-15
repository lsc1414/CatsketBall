using UnityEngine;
using System;

public class Ball : MonoBehaviour, IResetable, IAudioPlayable, IToggleable
{
	[SerializeField] private Vector3 startPosition;
	private bool isTimeUp;
	private Rigidbody2D rb;
	private AudioSource audioSource;

	public event EventHandler OnFinalBounce;

	public void Reset()
	{
		isTimeUp = false;
		transform.position = startPosition;
		transform.rotation = new Quaternion();
		rb.velocity = new Vector2(0, 0);
		rb.angularVelocity = 0;
	}

	public void Toggle(bool isActive)
	{
		gameObject.SetActive(isActive);
		rb.constraints = isActive ? RigidbodyConstraints2D.None : RigidbodyConstraints2D.FreezeAll;
	}

	private void Awake()
	{
		if (rb == null) rb = GetComponent<Rigidbody2D>();
		Reset();
	}

	public void PlayAudio()
	{
		if (audioSource.enabled) { audioSource.Play(); }
	}

	public void OnTimeIsUp()
	{
		isTimeUp = true;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (isTimeUp)
		{
			if (collision.gameObject.tag == "Floor")
			{
				isTimeUp = false;
				if (OnFinalBounce != null)
				{
					OnFinalBounce(this, e: new EventArgs());
				}
			}
		}
	}
}
