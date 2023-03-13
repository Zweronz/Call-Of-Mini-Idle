using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(TextMesh))]
public class TextAnimation : MonoBehaviour
{
	private IEnumerator animationLoop()
	{
		for (;;)
		{
			yield return new WaitForSeconds(timeBetweenFrames);
			if (animationIndex == frames.Length - 1)
			{
				if (stopWhenDone)
				{
					yield break;
				}
				animationIndex = 0;
			}
			else
			{
				animationIndex++;
			}
			GetComponent<AudioSource>().PlayOneShot(frameSound);
			GetComponent<TextMesh>().text = addTo ? GetComponent<TextMesh>().text + frames[animationIndex]  : frames[animationIndex];
		}
	}

	private void Start()
	{
		GetComponent<TextMesh>().text = frames[animationIndex];
		StartCoroutine(animationLoop());
	}

	private int animationIndex;

	public string[] frames;

	public float timeBetweenFrames = 0.02f;

	public bool stopWhenDone;

	public bool addTo;

	public AudioClip frameSound;
}
