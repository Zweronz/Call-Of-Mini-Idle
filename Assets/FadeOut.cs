using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
	public float timeInSeconds;

	private float time;

	private TextMesh textMesh;

	void Start()
	{
		textMesh = GetComponent<TextMesh>();
	}

	void Update()
	{
		time += Time.deltaTime;

		textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, Mathf.InverseLerp(timeInSeconds, 0f, time));
	}
}
