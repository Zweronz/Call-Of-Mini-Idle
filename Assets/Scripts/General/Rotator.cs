using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] private Vector3 axes;

	private void Update()
	{
		transform.Rotate(axes * Time.deltaTime);
	}
}
