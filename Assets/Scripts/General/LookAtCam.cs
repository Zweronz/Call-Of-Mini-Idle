using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class LookAtCam : MonoBehaviour
{
	void Update()
	{
		this.transform.LookAt(Game.RCInstance.curCamera().transform);
	}
}
