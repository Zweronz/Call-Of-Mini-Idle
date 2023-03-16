using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
	private void Start()
	{
		if (PlayerPrefs.GetInt("setDefaultVolume") == 0)
		{
			Global.currentVolume = 100f;
			PlayerPrefs.SetInt("setDefaultVolume", 1);
		}
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
