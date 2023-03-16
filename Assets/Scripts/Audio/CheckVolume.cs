using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVolume : MonoBehaviour
{
	void Update()
	{
		mSource.volume = Global.mute ? 0f : Global.currentVolume / 100f;
	}

	private AudioSource mSource;

	void Start()
	{
		mSource = GetComponent<AudioSource>();
	}
}
