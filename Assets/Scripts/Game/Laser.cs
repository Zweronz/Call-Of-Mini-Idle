using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class Laser : MonoBehaviour
{
	public GameObject start;

	public LayerMask layerMask;

	private Vector3 newScale;

	void Start()
	{
		newScale = new Vector3(0.1f, 0.1f, 0f);
		RaycastHit hit;
		this.transform.localScale = newScale;
		if (Physics.Raycast(start.transform.position, this.transform.forward, out hit, 99f, layerMask))
		{
			newScale.z = Vector3.Distance(hit.point, this.transform.position) / 44f;
			GameObject obj = Instantiate(GameAsset.Load<GameObject>("zombile3d_laser"), new Vector3(hit.point.x + 0.7f, hit.point.y, hit.point.z), Quaternion.identity);
			obj.transform.parent = hit.transform;
			RunController.instance.EffectsCache.Add(obj);
		}
	}

	void Update()
	{
		RaycastHit hit;
		this.transform.localScale = newScale;
		if (Physics.Raycast(start.transform.position, this.transform.forward, out hit, 99f, layerMask))
		{
			newScale.z = Vector3.Distance(hit.point, this.transform.position) / 44f;
			GameObject obj = Instantiate(GameAsset.Load<GameObject>("zombile3d_laser"), new Vector3(hit.point.x + 0.7f, hit.point.y, hit.point.z), Quaternion.identity);
			obj.transform.parent = hit.transform;
			RunController.instance.EffectsCache.Add(obj);
		}
	}
}
