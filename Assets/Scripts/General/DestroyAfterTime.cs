using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class DestroyAfterTime : MonoBehaviour
{
	public float time;
	
	void Start()
	{
		StartCoroutine(dothe());
	}

	void OnDisable()
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

	public IEnumerator dothe()
	{
		yield return new WaitForSeconds(time);
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
