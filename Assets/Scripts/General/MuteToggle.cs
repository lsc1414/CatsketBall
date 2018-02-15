using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour, IToggleable
{

	[SerializeField] private Image soundWaveImage;
	private AudioSource[] audios;

	public void Toggle(bool isActive)
	{
		audios = GameObject.FindObjectsOfType<AudioSource>();
		soundWaveImage.gameObject.SetActive(isActive);
		foreach (AudioSource audio in audios)
		{
			audio.enabled = isActive;
		}
	}

}
