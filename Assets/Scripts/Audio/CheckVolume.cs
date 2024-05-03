using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVolume : MonoBehaviour
{
	void Update()
	{
		source.volume = Global.mute ? 0f : Global.currentVolume / 100f;
	}

	private AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource>();
	}
}
