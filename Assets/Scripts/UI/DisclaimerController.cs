using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisclaimerController : MonoBehaviour
{
	public GameObject loadingUi;

	private bool done;

	void Update()
	{
		RaycastHit hit;
		Ray ray3 = GameObject.FindGameObjectWithTag("3DUI Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray3, out hit) && Input.GetMouseButtonDown(0) && !done)
		{
			Global.InstantiateTouchParticles(hit);
			GameObject.Find("DisclaimerUI").GetComponent<Animation>().Play((Application.loadedLevelName == "credits") ? "CreditsClose" :"DisclaimerEnd");
			if (Application.loadedLevelName == "credits")
			{
				LoadingUI.SetActive(false);
			}
			done = true;
		}
	}

	public void DoTheLoad(string scene)
	{
		GameObject.Find("3DUI/DisclaimerUI").SetActive(false);
		LoadingUI.LoadLevel(scene);
	}
}
