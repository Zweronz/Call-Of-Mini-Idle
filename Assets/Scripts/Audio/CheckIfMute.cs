using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMute : MonoBehaviour
{
	void Update()
	{
		mSource.volume = Global.currentVolume / 100f;
		if (Global.mute)
		{
			mSource.enabled = false;
		}
	}

	private AudioSource mSource;

	void Start()
	{
		mSource = GetComponent<AudioSource>();
	}
}
