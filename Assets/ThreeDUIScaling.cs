using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDUIScaling : MonoBehaviour
{
	void Update()
	{
		float xScale = (float)Screen.width / Screen.height * 0.5f * 1.125f;
		transform.localScale = new Vector3(xScale, 1f, 1f);

		foreach (Transform immediateChild in GetChildList(firstChild ? transform.GetChild(0) : transform.GetChild(0).GetChild(0)))
		{
			foreach (Transform panelChild in GetChildList(immediateChild))
			{
				if (!originalScales.ContainsKey(panelChild.gameObject.GetInstanceID()))
				{
					originalScales.Add(panelChild.gameObject.GetInstanceID(), panelChild.localScale);
				}

				bool yScaled = panelChild.tag == "YScaled";

				Vector3 scale = originalScales[panelChild.gameObject.GetInstanceID()];
				panelChild.localScale = new Vector3(scale.x, yScaled ? scale.y * xScale : scale.y, yScaled ? scale.z : scale.z * xScale);
				
				if (!originalPositions.ContainsKey(panelChild.gameObject.GetInstanceID()))
				{
					originalPositions.Add(panelChild.gameObject.GetInstanceID(), panelChild.localPosition);
				}

				Vector3 position = originalPositions[panelChild.gameObject.GetInstanceID()];
				panelChild.transform.localPosition = new Vector3(position.x, position.y, position.z);
			}
		}
	}

	public bool firstChild;

	public Transform center;

	public BoxCollider uiCollider;

	private Dictionary<int, Vector3> originalScales = new Dictionary<int, Vector3>();

	private Dictionary<int, Vector3> originalPositions = new Dictionary<int, Vector3>();

	private List<Transform> GetChildList(Transform root)
	{
		List<Transform> children = new List<Transform>();

		for (int i = 0;; i++)
		{
			try
			{
				Transform child = root.GetChild(i);

				if (child == null)
				{
					break;
				}

				children.Add(child);
			}
			catch
			{
				break;
			}
		}

		return children;
	}
}
