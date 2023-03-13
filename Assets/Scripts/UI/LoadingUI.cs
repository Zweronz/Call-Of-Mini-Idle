using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
	public static LoadingUI instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject ui = Instantiate(GameAsset.Load<GameObject>("LoadingUI"), GameObject.FindGameObjectWithTag("3DUI Camera").transform);
				LoadingUI loading = ui.GetComponent<LoadingUI>();
				ui.name = ui.name.Replace("(Clone)", "");
				ui.SetActive(false);
				_instance = loading;
			}
			return _instance;
		}
	}

	public static LoadingUI _instance;

	public GameObject fillBar;

	public static void LoadLevel(string scene)
	{
		SetActive(true);
		instance.StartCoroutine(LoadLevelRoutine(scene));
	}

	public static void SetActive(bool active)
	{
		instance.gameObject.SetActive(active);
	}

	public static IEnumerator LoadLevelRoutine(string scene)
	{
		yield return null;
        AsyncOperation asyncOperation = Application.LoadLevelAsync(scene);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
			instance.fillBar.transform.localScale = new Vector3(asyncOperation.progress * 1.12f, 1f, 1f);
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
	}
}
