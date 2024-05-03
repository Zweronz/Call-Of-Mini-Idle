using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class IdleThreeDeeButton : MonoBehaviour
{
	[SerializeField] public Material pressedMat;

	[SerializeField] public AudioClip pressSound;

	private Material originalMaterial;

	private bool shouldChangeMat;

	public bool repeatButton;

	public bool changeMat = true;

	public int methodGroup = 1;

	public void OnClicked()
	{
		StartCoroutine(click());
	}

	IEnumerator click()
	{
		AudioController.instance.PlayClip(pressSound, new Vector2(0.99f, 1.01f));
		Material mat = GetComponent<MeshRenderer>().material;
		if (changeMat)
		{
			GetComponent<MeshRenderer>().material = pressedMat;
		}
		while (Input.GetMouseButton(0))
		{
			yield return new WaitForEndOfFrame();
			if (repeatButton)
			{
				RunController.instance.SendMessage("ButtonsGroup" + methodGroup, this.name);
			}
		}
		if (changeMat)
		{
			shouldChangeMat = true;
		}
		if (!repeatButton)
		{
			RunController.instance.SendMessage("ButtonsGroup" + methodGroup, this.name);
		}
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
}
