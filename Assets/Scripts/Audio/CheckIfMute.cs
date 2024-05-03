using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMute : MonoBehaviour
{
	void Update()
	{
		source.volume = Global.currentVolume / 100f;
		source.enabled = !Global.mute;
	}

	private AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource>();
	}
}
