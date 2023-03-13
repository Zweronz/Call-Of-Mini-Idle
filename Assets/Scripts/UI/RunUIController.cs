using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunUIController : MonoBehaviour
{
	public List<TextMesh> labels;

	public GameObject[] UILayers;

	public GameObject[] ShopUILayers;

	public GameObject[] SettingsUILayers;

	public GameObject EnemyHealthBar;

	public TextMesh GetLabel(string name)
	{
		return labels.Find(x => x.name == name);
	}

	void Update()
	{
		RaycastHit hit;
		Ray ray3 = GameObject.FindGameObjectWithTag("3DUI Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray3, out hit) && Input.GetMouseButtonDown(0))
		{
			if (hit.transform.GetComponent<IdleThreeDeeButton>())
			{
				Instantiate(GameAsset.Load<GameObject>("TouchParticles"), hit.transform).transform.position = hit.point;
				hit.transform.SendMessage("OnClicked");
			}
			else
			{
				Instantiate(GameAsset.Load<GameObject>("TouchParticles"), hit.transform).transform.position = hit.point;
			}
		}
	}

	public void DoTheLoad(string scene)
	{
		LoadingUI.LoadLevel(scene);
	}
}
