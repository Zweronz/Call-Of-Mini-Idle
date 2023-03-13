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

	IEnumerator ShakeLoop()
	{
		for (;;)
		{
			yield return new WaitForEndOfFrame();
			if (shake)
			{
				for(;shake;)
				{
					yield return new WaitForEndOfFrame();
					this.transform.position = new Vector3(this.transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f), this.transform.position.y + UnityEngine.Random.Range(-0.1f, 0.1f), this.transform.position.z + UnityEngine.Random.Range(-0.1f, 0.1f));
				}
				while (this.transform.localPosition.x > 0.035f || this.transform.localPosition.x < -0.035f || this.transform.localPosition.y > 0.035f || this.transform.localPosition.y < -0.035f || this.transform.localPosition.z > 0.035f || this.transform.localPosition.z < -0.035f)
				{
					yield return new WaitForEndOfFrame();
					Vector3 v3 = this.transform.localPosition;
					if (v3.x < 0)
					{
						v3.x += Time.deltaTime * 2;
					}
					else if (v3.x > 0)
					{
						v3.x -= Time.deltaTime * 2;
					}
					if (v3.y < 0)
					{
						v3.y += Time.deltaTime * 2;
					}
					else if (v3.y > 0)
					{
						v3.y -= Time.deltaTime * 2;
					}
					if (v3.z < 0)
					{
						v3.z += Time.deltaTime * 2;
					}
					else if (v3.z > 0)
					{
						v3.z -= Time.deltaTime * 2;
					}
					this.transform.localPosition = v3;
				}
				this.transform.localPosition = Vector3.zero;
			}
		}
	}
}
