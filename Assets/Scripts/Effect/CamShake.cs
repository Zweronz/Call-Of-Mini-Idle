using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
	public bool shake;
	
	private bool reset;

	void Start()
	{
		StartCoroutine(ShakeLoop());
	}

	//WHAT.
	//I will get to whatever I am looking at later

	IEnumerator ShakeLoop()
	{
		while (true)
		{
			yield return new WaitForEndOfFrame();
			if (shake)
			{
				while (shake)
				{
					yield return new WaitForEndOfFrame();

					float random = UnityEngine.Random.Range(-7f, 7f);

					transform.localRotation = Quaternion.Euler(0, 0, random);
					transform.localPosition = new Vector3(random * 0.01f * UnityEngine.Random.Range(0.8f, 1f), random * 0.005f, random * 0.0025f);
				}

				transform.localRotation = Quaternion.identity;
				transform.localPosition = Vector3.zero;
			}
		}
	}
}
