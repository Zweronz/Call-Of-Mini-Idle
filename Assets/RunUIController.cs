using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunUIController : MonoBehaviour
{
	public List<TextMesh> labels;

	public GameObject[] UILayers;

	public GameObject LoadingUI;

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
				Instantiate(Resources.Load<GameObject>("TouchParticles"), hit.transform).transform.position = hit.point;
				hit.transform.SendMessage("OnClicked");
			}
			else
			{
				Instantiate(Resources.Load<GameObject>("TouchParticles"), hit.transform).transform.position = hit.point;
			}
		}
	}

	public void DoTheLoad(string scene)
	{
		StartCoroutine(LoadScene(scene));
	}

	public IEnumerator LoadScene(string scene)
    {
        yield return null;
		if (!Global.mute && !Global.muteMusic)
		{
			while (GameObject.Find("3DUI/LoadingUI").GetComponent<AudioSource>().volume < 1f)
			{
				GameObject.Find("3DUI/LoadingUI").GetComponent<AudioSource>().volume += Time.deltaTime * 4;
			}
		}
        AsyncOperation asyncOperation = Application.LoadLevelAsync(scene);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
			GameObject.Find("3DUI/LoadingUI/Panel/LoadBar/Fill").transform.localScale = new Vector3(asyncOperation.progress * 1.12f, 1f, 1f);
            if (asyncOperation.progress >= 0.9f)
            {
				if (!Global.mute && !Global.muteMusic)
				{
					while (GameObject.Find("3DUI/LoadingUI").GetComponent<AudioSource>().volume > 0f)
					{
						GameObject.Find("3DUI/LoadingUI").GetComponent<AudioSource>().volume -= Time.deltaTime * 4;
					}
				}
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
