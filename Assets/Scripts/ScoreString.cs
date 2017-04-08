using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreString : MonoBehaviour 
{
	public float timer =2f;
	public float speed = 1f;

	public void SetText(string s)
	{
		GetComponent<TextMesh>().text = s;
		Invoke("DelMe", timer);
	}

	private void FixedUpdate()
	{
		transform.position += new Vector3(0f, Time.deltaTime*speed, 0f);
	}

	private void DelMe()
	{
		Destroy(this.gameObject);
	}
}
