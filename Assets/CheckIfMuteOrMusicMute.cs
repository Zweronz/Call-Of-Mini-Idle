using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMuteOrMusicMute : MonoBehaviour
{
	void Update ()
	{
		if (Global.mute || Global.muteMusic)
		{
			GetComponent<AudioSource>().enabled = false;
		}
	}
}
