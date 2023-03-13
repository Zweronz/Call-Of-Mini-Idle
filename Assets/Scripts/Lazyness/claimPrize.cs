using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class claimPrize : MonoBehaviour
{
	string[] the = new string[]
	{
		"tiob784", "spidermanPillow120978", "bug heroes9375", "wow ! you luccy ! 39484", "sheesh3948", "machinegamer728", "bigdox7783", "check out fedesito902384", "wow ! you unluccky..."
	};
	void Start ()
	{
		GetComponent<TextMesh>().text = "please, @enderpurson with a screenshot. also, here's a code: \n" + the[UnityEngine.Random.Range(0, the.Length)];
	}
}
