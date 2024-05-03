using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMuteOrMusicMute : MonoBehaviour
{
	void Update()
	{
		source.volume = Global.currentVolume / 100f;
		source.enabled = !(Global.mute || Global.muteMusic);
	}

	private AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource>();
	}
}
