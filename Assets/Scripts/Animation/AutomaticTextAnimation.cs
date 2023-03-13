using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AutomaticTextAnimation : MonoBehaviour
{
	[SerializeField] private float timeBetweenFrames = 0.02f;

	[SerializeField] private bool startOnAwake = true;

	[SerializeField] private AudioClip frameSound;

	private string storedText;

	[HideInInspector] public bool start;

	void Start()
	{
		storedText = GetComponent<TextMesh>().text;
		if (startOnAwake)
		{
			InitializeAnimation();
			return;
		}
		GetComponent<TextMesh>().text = "";
	}

	void Update()
	{
		if (!start)
		{
			return;
		}
		InitializeAnimation();
		start = false;
	}

	public void InitializeAnimation()
	{
		TextAnimation anim = gameObject.AddComponent<TextAnimation>();
		anim.frames = storedText.ToCharArray().Select(c => c.ToString()).ToArray();
		anim.timeBetweenFrames = timeBetweenFrames;
		anim.stopWhenDone = true;
		anim.addTo = true;
		if (frameSound != null)
		{
			anim.frameSound = frameSound;
		}
		Destroy(this);
	}
}
