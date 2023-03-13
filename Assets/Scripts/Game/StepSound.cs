using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class StepSound : MonoBehaviour {
public AudioClip stepSound;
public float volume;
public bool tank;
public bool eliteTank;
public void Step()
{
	Game.ACInstance.PlayClip(stepSound, new Vector2(0.9f, 1.1f), AudioPlayType.normal, volume);
	if (tank)
	{
		if (eliteTank)
		{
			Game.RCInstance.doCamShake(0.07f);
			return;
		}
		Game.RCInstance.doCamShake(0.05f);
	}
}
}
