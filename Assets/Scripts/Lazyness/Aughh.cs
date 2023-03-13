using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aughh : MonoBehaviour
{
	public void Bruh()
	{
		GameObject.Find("3DUI").GetComponent<DisclaimerController>().DoTheLoad("menu");
	}

	public void bruh2()
	{
		GameObject.Find("3DUI").GetComponent<DisclaimerController>().DoTheLoad("funny");
	}
}
