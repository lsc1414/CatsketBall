using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour
{

	[SerializeField] private Image soundWaveImage;
	private AudioSource[] audios;

	public void Mute(bool isMuted)
	{
		audios = GameObject.FindObjectsOfType<AudioSource>();
		soundWaveImage.gameObject.SetActive(isMuted);
		foreach (AudioSource audio in audios)
		{
			audio.enabled = isMuted;
		}
	}

}
