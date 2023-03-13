using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(AudioSource))]
public class AnimationCommands : MonoBehaviour
{
	public void PlayAudio(AudioClip clipToPlay)
	{
		GetComponent<AudioSource>().PlayOneShot(clipToPlay);
	}
}
