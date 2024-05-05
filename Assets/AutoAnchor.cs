using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAnchor : MonoBehaviour
{
	void OnEnable()
	{
		ThreeDUIScaling.instance.AnchorObject(ThreeDUIScaling.GetScale(), transform);
	}
}
