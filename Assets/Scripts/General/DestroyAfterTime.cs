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
			RunController.instance.EffectsCache.RemoveAt(RunController.instance.EffectsCache.IndexOf(base.gameObject));
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
			RunController.instance.EffectsCache.RemoveAt(RunController.instance.EffectsCache.IndexOf(base.gameObject));
		}
		catch
		{
			Debug.Log("unfound");
		}
		Destroy(base.gameObject);
	}
}
