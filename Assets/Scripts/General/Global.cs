using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
	public static bool mute
	{
		get
		{
			return Storager.GetInt("mute") == 1;
		}
		set
		{
			if (value)
			{
				Storager.SetInt("mute", 1);
				return;
			}
			Storager.SetInt("mute", 0);
		}
	}
	public static bool muteMusic
	{
		get
		{
			return Storager.GetInt("muteMusic") == 1;
		}
		set
		{
			if (value)
			{
				Storager.SetInt("muteMusic", 1);
				return;
			}
			Storager.SetInt("muteMusic", 0);
		}
	}

	public static float currentVolume
	{
		get
		{
			return Storager.GetFloat("curVolume");
		}
		set
		{
			Storager.SetFloat("curVolume", value);
		}
	}
	public static Global objectInstance
	{
		get
		{
			if (GameObject.FindGameObjectWithTag("GlobalObject") ==  null)
			{
				_instance = CreateGlobal();
			}
			return _instance;
		}
	}

	private static Global _instance;

	private static Global CreateGlobal()
	{
		GameObject global = new GameObject("GlobalObject");
		global.tag = "GlobalObject";
		global.AddComponent<Global>();
		DontDestroyOnLoad(global);
		return global.GetComponent<Global>();
	}

	private static float touchParticlesCooldown;

	private void Update()
	{
		if (touchParticlesCooldown >= 0f)
		{
			touchParticlesCooldown -= 1 * Time.deltaTime;
		}
	}

	public static void InstantiateTouchParticles(RaycastHit hit)
	{
		if (objectInstance == null)
		{
			Debug.Log("uh");
		}
		if (touchParticlesCooldown > 0f)
		{
			return;
		}
		touchParticlesCooldown = 0.1f;
		Instantiate(GameAsset.Load<GameObject>("TouchParticles"), hit.transform).transform.position = hit.point;
	}
}
