using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerChild : MonoBehaviour
{
	void Update()
	{
		if (!source.isPlaying || Global.mute)
		{
			AudioController.instance.tempSources.Remove(source);
			Destroy(gameObject);
		}
	}

	private AudioSource source
	{
		get
		{
			if (_source == null)
			{
				_source = GetComponent<AudioSource>();
			}

			return _source;
		}
	}

	private AudioSource _source;
}
