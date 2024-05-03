using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class StepSound : MonoBehaviour
{
	public AudioClip stepSound;
	
	public float volume;
	
	public bool tank;
	
	public bool eliteTank;
	
	public void Step()
	{
		AudioController.instance.PlayClip(stepSound, new Vector2(0.9f, 1.1f), AudioPlayType.Normal, volume);

		if (tank)
		{
			if (eliteTank)
			{
				RunController.instance.doCamShake(0.07f);
				return;
			}
			RunController.instance.doCamShake(0.05f);
		}
	}
}
