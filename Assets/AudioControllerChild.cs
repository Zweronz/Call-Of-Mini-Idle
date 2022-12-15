using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerChild : MonoBehaviour
{
	void Update()
	{
		if (!GetComponent<AudioSource>().isPlaying || Global.mute)
		{
			transform.parent.GetComponent<AudioController>().tempSources.RemoveAt(transform.parent.GetComponent<AudioController>().tempSources.IndexOf(this.gameObject.GetComponent<AudioSource>()));
			Destroy(gameObject);
		}
	}
}
