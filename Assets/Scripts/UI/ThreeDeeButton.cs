using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class ThreeDeeButton : MonoBehaviour
{
	[SerializeField]
	public Material pressedMat;

	[SerializeField]
	public AudioClip pressSound;

	private Material originalMaterial;

	private bool shouldChangeMat;

	public bool repeatButton;

	public void OnClicked()
	{
		StartCoroutine(click());
	}

	IEnumerator click()
	{
		Game.ACInstance.PlayClip(pressSound, new Vector2(0.99f, 1.01f));
		Material mat = GetComponent<MeshRenderer>().material;
		GetComponent<MeshRenderer>().material = pressedMat;
		while (Input.GetMouseButton(0))
		{
			yield return new WaitForEndOfFrame();
			if (repeatButton)
			{
				this.SendMessage(this.name);
			}
		}
		if (!repeatButton)
		{
			shouldChangeMat = true;
		}
		this.SendMessage(this.name);
	}

	void Start()
	{
		originalMaterial = GetComponent<MeshRenderer>().material;
	}

	void Update()
	{
		if (shouldChangeMat)
		{
			GetComponent<MeshRenderer>().material = originalMaterial;
			shouldChangeMat = false;
		}
	}

	public void Menu_Start()
	{
		GameObject.Find("Pivot/3DUI/MenuUI").SetActive(false);
		LoadingUI.LoadLevel("funny");
	}

	public void Menu_Exit()
	{
		Application.Quit();
	}

	public void Menu_Settings()
	{
		GameObject.Find("Pivot/3DUI/MenuUI").GetComponent<Animation>().Play("ScrollToSettings");
	}

	public void Settings_Back()
	{
		GameObject.Find("Pivot/3DUI/MenuUI").GetComponent<Animation>().Play("ScrollBackFromSettings");
	}
}
