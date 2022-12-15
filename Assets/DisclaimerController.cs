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
			Instantiate(Resources.Load<GameObject>("TouchParticles"), hit.transform).transform.position = hit.point;
			GameObject.Find("DisclaimerUI").GetComponent<Animation>().Play("DisclaimerEnd");
			done = true;
		}
	}

	public void DoTheLoad(string scene)
	{
		GameObject.Find("3DUI/DisclaimerUI").SetActive(false);
		loadingUi.SetActive(true);
		StartCoroutine(LoadScene(scene));
	}

	public IEnumerator LoadScene(string scene)
    {
        yield return null;
		while (GameObject.Find("3DUI/LoadingUI").GetComponent<AudioSource>().volume < 1f)
		{
			GameObject.Find("3DUI/LoadingUI").GetComponent<AudioSource>().volume += Time.deltaTime * 4;
		}
        AsyncOperation asyncOperation = Application.LoadLevelAsync(scene);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
			GameObject.Find("3DUI/LoadingUI/Panel/LoadBar/Fill").transform.localScale = new Vector3(asyncOperation.progress * 1.12f, 1f, 1f);
            if (asyncOperation.progress >= 0.9f)
            {
				while (GameObject.Find("3DUI/LoadingUI").GetComponent<AudioSource>().volume > 0f)
				{
					GameObject.Find("3DUI/LoadingUI").GetComponent<AudioSource>().volume -= Time.deltaTime * 4;
				}
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
