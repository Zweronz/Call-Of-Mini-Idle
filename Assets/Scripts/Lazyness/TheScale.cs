using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheScale : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Vector3 v3 = new Vector3((float)Screen.width / 9787f, (float)Screen.height / 5559f, 0.1205722f);
		this.transform.localScale = v3;
	}
}
