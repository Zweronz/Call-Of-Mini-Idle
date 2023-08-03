using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	void Update()
	{
		RaycastHit hit;
		Ray ray3 = GameObject.FindGameObjectWithTag("3DUI Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray3, out hit) && Input.GetMouseButtonDown(0))
		{
			if (hit.transform.GetComponent<ThreeDeeButton>())
			{
				Global.InstantiateTouchParticles(hit);
				hit.transform.SendMessage("OnClicked");
			}
			else
			{
				Global.InstantiateTouchParticles(hit);
			}
		}
	}

	public void DoTheLoad(string scene)
	{
		LoadingUI.LoadLevel(scene);
	}
}