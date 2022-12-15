using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	void Update ()
	{
		RaycastHit hit;
		Ray ray3 = GameObject.FindGameObjectWithTag("3DUI Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray3, out hit) && hit.transform.GetComponent<ThreeDeeButton>() && Input.GetMouseButtonDown(0))
		{
			hit.transform.SendMessage("OnClicked");
		}
	}
}
