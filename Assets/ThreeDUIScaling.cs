using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDUIScaling : MonoBehaviour
{
	void Update()
	{
		transform.localScale = new Vector3(Screen.width * (0.00039125f), Screen.height * (0.000765f), 1f);
	}
}
