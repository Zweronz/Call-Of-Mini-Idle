using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testHitboxThing : MonoBehaviour
{
	private GameObject currentButton;

	private Vector2 screenThing = new Vector2((float)Screen.width / 102.875f, (float)Screen.height / 56.875f);

	private Vector2 screenThing2 = new Vector2((float)Screen.width / 32f, (float)Screen.height / 12.55f);

	void ClickButton()
	{
		if (currentButton == null)
		{
			return;
		}
		currentButton.SendMessage("OnClicked");
	}

	void Update()
	{
		Debug.LogError(Screen.width + "x" + Screen.height);
		Debug.LogError(screenThing2.y);
		Vector3 v3 = new Vector3(Input.mousePosition.x / screenThing.x - screenThing2.x, Input.mousePosition.y / screenThing.y - screenThing2.y, 0f);
		this.transform.localPosition = v3;
		if (Input.GetMouseButtonDown(0))
		{
			ClickButton();
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (!c.gameObject.GetComponent<ThreeDeeButton>())
		{
			return;
		}
		currentButton = c.gameObject;
	}

	void OnTriggerExit(Collider c)
	{
		currentButton = null;
	}
}
