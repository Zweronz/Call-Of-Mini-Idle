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
			Game.RCInstance.EffectsCache.RemoveAt(Game.RCInstance.EffectsCache.IndexOf(base.gameObject));
		}
		catch
		{
			Debug.Log("unfound");
		}
		Destroy(base.gameObject);
	}
}
