using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COMIdleStage1
{
	public class Game
	{
		public static AudioController ACInstance
		{
			get
			{
				return GameObject.Find("AudioController").GetComponent<AudioController>();
			}
		}

		public static RunController RCInstance
		{
			get
			{
				return GameObject.Find("RunController").GetComponent<RunController>();
			}
		}
	}
}
