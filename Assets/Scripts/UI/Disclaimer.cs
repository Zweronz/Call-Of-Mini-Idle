using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disclaimer : MonoBehaviour
{
	public GUIStyle disclaimer;

	void Start()
	{
		Application.targetFrameRate = 60;
	}

	void OnGUI()
	{
		if (GUI.Button(Utilities.screenScaleRect(0f, 0f, 1f, 1f), string.Empty, disclaimer))
		{
			Application.LoadLevel("menu");
		}
	}
}
