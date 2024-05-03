using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class Destroy : MonoBehaviour
{
	public void DestroyObj()
	{
		try
		{
			RunController.instance.EffectsCache.RemoveAt(RunController.instance.EffectsCache.IndexOf(base.gameObject));
		}
		catch
		{
			Debug.Log("unfound");
		}
		Destroy(base.gameObject);
	}
}
