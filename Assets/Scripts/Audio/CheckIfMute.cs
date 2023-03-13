using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMute : MonoBehaviour
{
	void Update()
	{
		if (Global.mute)
		{
			GetComponent<AudioSource>().enabled = false;
		}
	}
}
