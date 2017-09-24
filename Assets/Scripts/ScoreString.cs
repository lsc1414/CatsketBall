using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreString : MonoBehaviour 
{
	public float timer =2f;
	public float speed = 1f;
	[SerializeField] private TextMesh subMesh;

	public void SetText(string s)
	{
		MeshRenderer rend = GetComponent<MeshRenderer>();
		MeshRenderer subRend = subMesh.GetComponent<MeshRenderer>();
		rend.sortingLayerName = "TextMesh";
		subRend.sortingLayerName = "TextMesh";
		rend.sortingOrder = 1;
		subRend.sortingOrder = 0;
		GetComponent<TextMesh>().text = s;
		subMesh.GetComponent<TextMesh>().text = s;
		Invoke("DelMe", timer);
	}

	private void FixedUpdate()
	{
		transform.position += new Vector3(0f, Time.deltaTime*speed, 0f);
		subMesh.transform.localPosition = new Vector3 (0.01F, -0.01F, 0F);
	}

	private void DelMe()
	{
		Destroy(this.gameObject);
	}
}
