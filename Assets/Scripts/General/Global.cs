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
}
