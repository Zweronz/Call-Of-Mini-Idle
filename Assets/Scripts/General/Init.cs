using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
	private void Start()
	{
		GameAsset.InitializeAssets();
	}

	private void Update()
	{
		statusLabel.text = GameAsset.status;
		if (GameAsset.status == "Finished.")
		{
			Application.LoadLevel("disclaimer");
		}
	}

	[SerializeField] private TextMesh statusLabel;
}
