using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class RunController : MonoBehaviour
{

	public Texture theThing2;

	public Transform deadZombieZone;

	public Texture theThing;

	public GUIStyle font;

	public GUIStyle font2;

	public GUIStyle button;

	private float the = 100;

	public GUIStyle settingsButton;

	public GUIStyle plus;

	public GUIStyle minus;

	public GUIStyle arrowLeft;

	public GUIStyle arrowRight;

	public AudioClip buttonSound;

	public AudioClip buySound;

	private bool shopOpen;

	public Texture bg;

	public Container[] cons;
	
	public AudioController audioCon;

	public MovingCam MC;

	public Texture header;

	private bool settingsOpen;

	private bool shotgun;

	private bool rpg;

	public Texture testTexture;

	public List<GameObject> EffectsCache = new List<GameObject>();

	public void ClearEffectsCache()
	{
		foreach (GameObject obj in EffectsCache)
		{
			Destroy(obj);
		}
		EffectsCache.Clear();
	}

	public int TotalUnlockedItems
	{
		get
		{
			return PlayerPrefs.GetInt("totalunlockeditems");
		}
		set
		{
			PlayerPrefs.SetInt("totalunlockeditems", value);
		}
	}

	public int FOV
	{
		get
		{
			return PlayerPrefs.GetInt("fov");
		}
		set
		{
			PlayerPrefs.SetInt("fov", value);
		}
	}

	public string myTrueZombie
	{
		get
		{
			return PlayerPrefs.GetString("myzombie");
		}
	}

	public string myZombie
	{
		get
		{
			return PlayerPrefs.GetString("myzombie");
		}
		set
		{
			PlayerPrefs.SetString("myzombie", value);
		}
	}

	public bool eliteZombie
	{
		get
		{
			return PlayerPrefs.GetInt("shopelitezombie") == 1;
		}
		set
		{
			if (!value)
			{
				PlayerPrefs.SetInt("shopelitezombie", 0);
				return;
			}
			PlayerPrefs.SetInt("shopelitezombie", 1);
		}
	}

	public string myHuman
	{
		get
		{
			return PlayerPrefs.GetString("myhuman");
		}
		set
		{
			PlayerPrefs.SetString("myhuman", value);
		}
	}

	public string myGun
	{
		get
		{
			return PlayerPrefs.GetString("mygun");
		}
		set
		{
			PlayerPrefs.SetString("mygun", value);
		}
	}

	public bool[] shopBools =
	{
		false,
		false,
		false,
		false,
		false
	};

	public int[] shopIndex =
	{
		0,
		0,
		0,
		0,
		0
	};

	public bool[] settingsBools =
	{
		false,
		false,
		false,
		false,
		false
	};

	public void pbs()
	{
		Game.ACInstance.PlayClip(buttonSound, new Vector2(0.98f, 1.02f));
	}

	public void pbs2()
	{
		Game.ACInstance.PlayClip(buySound, new Vector2(0.98f, 1.02f));
	}

	public Camera curCamera()
	{
		if (PlayerPrefs.GetInt("fpscam") == 0)
		{
			return MC.DetachedCam;
		}
		else
		{
			return MC.guy.GetComponent<HumanStats>().fpsCam;
		}
	}

	public bool gs(string s)
	{
		return PlayerPrefs.GetInt("shop" + s) == 1;
	}

	public void ss(string s, int i)
	{
		PlayerPrefs.SetInt("shop" + s, i);
	}

	public float RunPoints
	{
		get
		{
			return PlayerPrefs.GetFloat("runpoints");
		}
		set
		{
			PlayerPrefs.SetFloat("runpoints", value);
		}
	}

	public float RunPointsPerSecond
	{
		get
		{
			return PlayerPrefs.GetFloat("rps") + RPSFromZombie + RPSFromHuman;
		}
		set
		{
			PlayerPrefs.SetFloat("rps", value + RPSFromZombie + RPSFromHuman);
		}
	}

	public float RPSFromZombie
	{
		get
		{
			return PlayerPrefs.GetFloat("rpszombie");
		}
		set
		{
			PlayerPrefs.SetFloat("rpszombie", value);
		}
	}

	public float RPSFromHuman
	{
		get
		{
			return PlayerPrefs.GetFloat("rpshuman");
		}
		set
		{
			PlayerPrefs.SetFloat("rpshuman", value);
		}
	}

	public string CurrentMusic
	{
		get
		{
			return PlayerPrefs.GetString("currentmusic");
		}
		set
		{
			PlayerPrefs.SetString("currentmusic", value);
		}
	}

	public bool fpsCam
	{
		get
		{
			return PlayerPrefs.GetInt("fpscam") == 1;
		}
		set
		{
			if (!value)
			{
				PlayerPrefs.SetInt("fpscam", 0);
				return;
			}
			PlayerPrefs.SetInt("fpscam", 1);
		}
	}

	public int LevelIndex
	{
		get
		{
			return PlayerPrefs.GetInt("levelindex");
		}
		set
		{
			PlayerPrefs.SetInt("levelindex", value);
		}
	}

	public float CurrentZombieHealth
	{
		get
		{
			return PlayerPrefs.GetFloat("zombiehealth");
		}
		set
		{
			PlayerPrefs.SetFloat("zombiehealth", value);
		}
	}

	public float MaximumZombieHealth
	{
		get
		{
			string str = (eliteZombie) ? "elite_" + myZombie : myZombie;
			return Resources.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().health;
		}
	}

	public float CurrentZombieTime
	{
		get
		{
			return PlayerPrefs.GetFloat("zombietime");
		}
		set
		{
			PlayerPrefs.SetFloat("zombietime", value);
		}
	}

	public float MaximumZombieTime
	{
		get
		{
			string str = (eliteZombie) ? "elite_" + myZombie : myZombie;
			return Resources.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().respawnTime;
		}
	}

	public void Buy(string key, int cost)
	{
		if (RunPoints < cost)
		{
			return;
		}
		if (RunPoints >= cost)
		{
			RunPoints -= cost;
			ss(key, 1);
			TotalUnlockedItems++;
		}
	}

	public void Shoot()
	{
		if (MC.guy.GetComponent<HumanStats>().isLaser)
		{
			foreach (ParticleRenderer rend in MC.guy.GetComponent<HumanStats>().weapon.GetComponent<WeaponStats>().emit)
			{
				rend.enabled = true;
			}
			if (MC.guy.GetComponent<HumanStats>().isFlamer)
			{
				foreach (ParticleEmitter emit2 in MC.guy.GetComponent<HumanStats>().weapon.GetComponent<WeaponStats>().emit2)
				{
					emit2.emit = true;
				}
			}
		}
		if (!MC.guy.GetComponent<HumanStats>().isShotgun)
		{
			RunPoints += Resources.Load<GameObject>("guns/" + myGun).GetComponent<WeaponStats>().damage;
			Utilities.InsGobj(Resources.Load<GameObject>("pluspoints"), MC.plusPointsPos[UnityEngine.Random.Range(0, MC.plusPointsPos.Length)]).GetComponent<TextMesh>().text = "+" + MC.guy.GetComponent<HumanStats>().weapon.GetComponent<WeaponStats>().damage;
			if (CurrentZombieHealth > 0f)
			{
				CurrentZombieHealth -= Resources.Load<GameObject>("guns/" + myGun).GetComponent<WeaponStats>().damage;
			}
			if (CurrentZombieHealth < 0f)
			{
				CurrentZombieHealth = 0f;
			}
		}
		else
		{
			float the = UnityEngine.Random.Range(Resources.Load<GameObject>("guns/" + myGun).GetComponent<WeaponStats>().randomDamage.x, Resources.Load<GameObject>("guns/" + myGun).GetComponent<WeaponStats>().randomDamage.y);
			Utilities.InsGobj(Resources.Load<GameObject>("pluspoints"), MC.plusPointsPos[UnityEngine.Random.Range(0, MC.plusPointsPos.Length)]).GetComponent<TextMesh>().text = "+" + Math.Round(the);
			RunPoints += the;
			if (CurrentZombieHealth > 0f)
			{
				CurrentZombieHealth -= the;
			}
			if (CurrentZombieHealth < 0f)
			{
				CurrentZombieHealth = 0f;
			}
		}
	}

	public IEnumerator Shotgun()
	{
		MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().Play("RunShoot01_Shotgun");
		shotgun = true;
		yield return new WaitForSeconds(0.5f);
		shotgun = false;
	}

	public IEnumerator RPG()
	{
		MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().Play("RunShoot01_RPG");
		rpg = true;
		yield return new WaitForSeconds(0.5f);
		rpg = false;
	}

	public void bz(string playerprefs, string prefab, int cost)
	{
		bool justbought = false;
		if (!gs(playerprefs))
		{
			Buy(playerprefs, cost);
			if (RunPoints >= cost)
			{
				justbought = true;
			}
		}
		if (myZombie != prefab && gs(playerprefs))
		{
			doZombie(prefab, false);
		}
		if (justbought)
		{
			pbs2();
			return;
		}
		pbs();
	}

	public void bc(string playerprefs, int cost)
	{
		bool justbought = false;
		if (!gs(playerprefs))
		{
			Buy(playerprefs, cost);
			if (RunPoints >= cost)
			{
				justbought = true;
			}
		}
		if (justbought)
		{
			pbs2();
			return;
		}
		pbs();
	}

	public void bw(string playerprefs, string prefab, int cost)
	{
		bool justbought = false;
		if (!gs(playerprefs))
		{
			Buy(playerprefs, cost);
			if (RunPoints >= cost)
			{
				justbought = true;
			}
		}
		if (myGun != prefab && gs(playerprefs))
		{
			doGun(prefab);
		}
		if (justbought)
		{
			pbs2();
			return;
		}
		pbs();
	}

	public void bh(string playerprefs, string prefab, int cost)
	{
		bool justbought = false;
		if (!gs(playerprefs))
		{
			Buy(playerprefs, cost);
			justbought = true;
		}
		if (myHuman != prefab && gs(playerprefs))
		{
			doHuman(prefab);
		}
		if (justbought && RunPoints >= cost)
		{
			pbs2();
			return;
		}
		pbs();
	}

	void OnGUI()
	{
		if (!shopOpen)
		{
			GUI.Label(Utilities.screenScaleRect(0.4835f, 0.0175f, 0.1f, 0.1f), "Enemy Health", font);
			GUI.DrawTexture(Utilities.screenScaleRect(0.475f, 0.1f, Utilities.Percentage(CurrentZombieHealth, MaximumZombieHealth) / 800f, 0.05f), testTexture);
			GUI.DrawTexture(Utilities.screenScaleRect(0.475f, 0.1f, 0.127f, 0.052f), theThing2);
			if (GUI.RepeatButton(Utilities.screenNoScaleRect(0.4f, 0.8f, 400f, 100f), string.Empty, button))
			{
				if (!MC.guy.GetComponent<HumanStats>().isShotgun && !MC.guy.GetComponent<HumanStats>().isRPG && !MC.guy.GetComponent<HumanStats>().isLaser)
				{
					if (!MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().IsPlaying("RunShoot01"))
					{
						MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().Play("RunShoot01");
					}
				}
				if (MC.guy.GetComponent<HumanStats>().isLaser)
				{
					if (!MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().IsPlaying("RunShoot01_Laser"))
					{
						MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().Play("RunShoot01_Laser");
					}
				}
				if (MC.guy.GetComponent<HumanStats>().isShotgun)
				{
					if (!shotgun)
					{
						StartCoroutine(Shotgun());
					}
				}
				if (MC.guy.GetComponent<HumanStats>().isRPG)
				{
					if(!rpg)
					{
						StartCoroutine(RPG());
					}
				}
			}
			else
			{
				if (GameObject.Find("Laser(Clone)") && !MC.guy.GetComponent<Animation>().IsPlaying("RunShoot01_Laser"))
				{
					try
					{
						Game.RCInstance.EffectsCache.RemoveAt(Game.RCInstance.EffectsCache.IndexOf(GameObject.Find("Laser(Clone)")));
					}
					catch
					{
						Debug.Log("unfound");
					}
					Destroy(GameObject.Find("Laser(Clone)"));
				}
			}
			GUI.Label(Utilities.screenNoScaleRect(0.4765f, 0.792f, 100f, 50f), "Shoot", font2);
			}
			GUI.DrawTexture(Utilities.screenScaleRect(0.05f, 0.025f, 0.2f, 0.15f), header);
			GUI.Label(Utilities.screenScaleRect(0.1035f, 0.0015f, 0.1f, 0.1f), "RunPoints", font);
			GUI.Label(Utilities.screenScaleRect(0.1175f, 0.075f, 0.1f, 0.1f), "" + Math.Round(RunPoints), font);
			GUI.DrawTexture(Utilities.screenScaleRect(0.25f, 0.025f, 0.2f, 0.15f), header);
			GUI.Label(Utilities.screenScaleRect(0.3275f, 0.0015f, 0.1f, 0.1f), "RPS", font);
			GUI.Label(Utilities.screenScaleRect(0.3175f, 0.075f, 0.1f, 0.1f), "" + RunPointsPerSecond, font);
			if (GUI.Button(Utilities.screenNoScaleRect(0.75f, 0.05f, 300f, 100f), string.Empty, button))
			{
				pbs();
				if (!settingsOpen)
				{
				shopOpen = !shopOpen;
				}
			}
			if (GUI.Button(Utilities.screenNoScaleRect(0.8f, 0.8f, 200f, 100f), string.Empty, settingsButton))
			{
				pbs();
				if (!shopOpen)
				{
				settingsOpen = !settingsOpen;
				}
		}
		GUI.Label(Utilities.screenNoScaleRect(0.8065f, 0.042f, 100f, 50f), "Shop", font2);
		if (settingsOpen)
		{
			GUI.DrawTexture(Utilities.screenScaleRect(0.2f, 0.15f, 0.58f, 0.775f), bg);
			if (GUI.Button(Utilities.screenNoScaleRect(0.22f, 0.2f, 300f, 100f), string.Empty, button))
			{
				pbs();
				DoTheSettingsBools(!settingsBools[0], 0);
			}
			GUI.Label(Utilities.screenNoScaleRect(0.275f, 0.1925f, 100f, 50f), "Music", font2);
			if (GUI.Button(Utilities.screenNoScaleRect(0.22f, 0.35f, 300f, 100f), string.Empty, button))
			{
				pbs();
				DoTheSettingsBools(!settingsBools[1], 1);
			}
			GUI.Label(Utilities.screenNoScaleRect(0.275f, 0.3425f, 100f, 50f), "Video", font2);
			if (settingsBools[0])
			{
				if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.2f, 300f, 100f), string.Empty, button))
				{
					pbs();
					if (CurrentMusic != "BGM1")
					{
					CurrentMusic = "BGM1";
					audioCon.ChangeClip(CurrentMusic);
					}
				}
				GUI.Label(Utilities.screenNoScaleRect(0.5925f, 0.195f, 100f, 50f), "COMZ BGM 1", font2);
				if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.35f, 300f, 100f), string.Empty, button))
				{
					pbs();
					if (CurrentMusic != "BGM2")
					{
					CurrentMusic = "BGM2";
					audioCon.ChangeClip(CurrentMusic);
					}
				}
				GUI.Label(Utilities.screenNoScaleRect(0.5925f, 0.3425f, 100f, 50f), "COMZ BGM 2", font2);
				if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.5f, 300f, 100f), string.Empty, button))
				{
					pbs();
					if (CurrentMusic != "MILLERINST")
					{
					CurrentMusic = "MILLERINST";
					audioCon.ChangeClip(CurrentMusic);
					}
				}
				GUI.Label(Utilities.screenNoScaleRect(0.6075f, 0.4925f, 100f, 50f), "Miller Inst", font2);
			}
			if (settingsBools[1])
			{
				if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.2f, 300f, 100f), string.Empty, button))
				{
					pbs();
					fpsCam = !fpsCam;
					doFpsCam(fpsCam);
				}
				GUI.Label(Utilities.screenNoScaleRect(0.5525f, 0.1325f, 100f, 50f), "Motion Sickness Warning!", font2);
				GUI.Label(Utilities.screenNoScaleRect(0.6025f, 0.1925f, 100f, 50f), "FPS Camera", font2);
				if (GUI.Button(Utilities.screenNoScaleRect(0.63f, 0.35f, 75f, 75f), string.Empty, plus))
				{
					pbs();
					if (FOV < 90)
					{
						FOV++;
					}
				}
				if (GUI.Button(Utilities.screenNoScaleRect(0.68f, 0.35f, 75f, 75f), string.Empty, minus))
				{
					pbs();
					if (FOV > 45)
					{
						FOV--;
					}
				}
				GUI.Label(Utilities.screenNoScaleRect(0.5525f, 0.3425f, 100f, 50f), "FOV(" + FOV + ")", font2);
			}
		}
		if (shopOpen)
		{
			GUI.DrawTexture(Utilities.screenScaleRect(0.2f, 0.15f, 0.58f, 0.775f), bg);
			if (GUI.Button(Utilities.screenNoScaleRect(0.22f, 0.2f, 300f, 100f), string.Empty, button))
			{
				pbs();
				DoTheShopBools(!shopBools[0], 0);
			//no
			}
			GUI.Label(Utilities.screenNoScaleRect(0.265f, 0.1925f, 100f, 50f), "Zombies", font2);
			if (GUI.Button(Utilities.screenNoScaleRect(0.22f, 0.35f, 300f, 100f), string.Empty, button))
			{
				pbs();
				DoTheShopBools(!shopBools[1], 1);
			}
			GUI.Label(Utilities.screenNoScaleRect(0.275f, 0.3425f, 100f, 50f), "Skins", font2);
			if (GUI.Button(Utilities.screenNoScaleRect(0.22f, 0.5f, 300f, 100f), string.Empty, button))
			{
				pbs();
				DoTheShopBools(!shopBools[2], 2);
			}
			GUI.Label(Utilities.screenNoScaleRect(0.26f, 0.4925f, 100f, 50f), "Weapons", font2);
			if (GUI.Button(Utilities.screenNoScaleRect(0.22f, 0.65f, 300f, 100f), string.Empty, button))
			{
				pbs();
				DoTheShopBools(!shopBools[3], 3);
			}
			GUI.Label(Utilities.screenNoScaleRect(0.2575f, 0.6425f, 100f, 50f), "Upgrades", font2);
			if (shopBools[3])
			{
				if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.2f, 300f, 100f), string.Empty, button))
				{
					pbs();
					bc("elitezombie", 100000);
					if (gs("elitezombie"))
					{
						doZombie("Zombie", false);
					}
				}
				if (gs("elitezombie"))
				{
					GUI.Label(Utilities.screenNoScaleRect(0.4675f, 0.195f, 100f, 50f), "Elite", font2);
				}
				else
				{
					GUI.Label(Utilities.screenNoScaleRect(0.455f, 0.195f, 100f, 50f), "100000", font2);
				}
			}
			if (shopBools[0])
			{
				if (shopIndex[0] == 0)
				{
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.2f, 300f, 100f), string.Empty, button))
					{
						pbs();
						if (myZombie != "Zombie")
						{
							doZombie("Zombie", false);
						}
					}
					GUI.Label(Utilities.screenNoScaleRect(0.455f, 0.195f, 100f, 50f), "Zombie", font2);
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.35f, 300f, 100f), string.Empty, button))
					{
						if (!eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieZombie") == 1)
						bz("dog", "Dog", 250);
					}
					if (gs("dog") && !eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieZombie") == 1)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.47f, 0.3425f, 100f, 50f), "Dog", font2);
					}
					else if (eliteZombie && PlayerPrefs.GetInt("killedzombieZombie") == 0)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.4550f, 0.3425f, 100f, 50f), "Kill Last", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.46f, 0.3425f, 100f, 50f), "250 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.5f, 300f, 100f), string.Empty, button))
					{
						if (!eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieDog") == 1)
						bz("nurse", "Nurse", 750);
					}
					if (gs("nurse") && !eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieDog") == 1)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.465f, 0.4925f, 100f, 50f), "Nurse", font2);
					}
					else if (eliteZombie && PlayerPrefs.GetInt("killedzombieDog") == 0)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.455f, 0.4925f, 100f, 50f), "Kill Last", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.46f, 0.4925f, 100f, 50f), "750 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.65f, 300f, 100f), string.Empty, button))
					{
						if (!eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieNurse") == 1)
						bz("policezombie", "Zombie_Police", 1500);
					}
					if (gs("policezombie") && !eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieNurse") == 1)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.4325f, 0.6425f, 100f, 50f), "Police Zombie", font2);
					}
					else if (eliteZombie && PlayerPrefs.GetInt("killedzombieNurse") == 0)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.4550f, 0.6425f, 100f, 50f), "Kill Last", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.6425f, 100f, 50f), "1500 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.8f, 300f, 100f), string.Empty, button))
					{
						if (!eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieZombie_Police") == 1)
						bz("swatzombie", "Zombie_Swat", 2500);
					}
					if (gs("swatzombie") && !eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieZombie_Police") == 1)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.4355f, 0.7925f, 100f, 50f), "Swat Zombie", font2);
					}
					else if (eliteZombie && PlayerPrefs.GetInt("killedzombieZombie_Police") == 0)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.4550f, 0.7925f, 100f, 50f), "Kill Last", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.7925f, 100f, 50f), "2500 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.2f, 300f, 100f), string.Empty, button))
					{
						if (!eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieZombie_Swat") == 1)
						bz("boomer", "Boomer", 4000);
					}
					if (gs("boomer") && !eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieZombie_Swat") == 1)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6165f, 0.195f, 100f, 50f), "Boomer", font2);
					}
					else if (eliteZombie && PlayerPrefs.GetInt("killedzombieZombie_Swat") == 0)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6150f, 0.195f, 100f, 50f), "Kill Last", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.615f, 0.195f, 100f, 50f), "4000 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.35f, 300f, 100f), string.Empty, button))
					{
						if (!eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieBoomer") == 1)
						bz("tank", "Tank", 7500);
					}
					if (gs("tank") && !eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieBoomer") == 1)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6265f, 0.345f, 100f, 50f), "Tank", font2);
					}
					else if (eliteZombie && PlayerPrefs.GetInt("killedzombieBoomer") == 0)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6150f, 0.345f, 100f, 50f), "Kill Last", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6175f, 0.345f, 100f, 50f), "7500 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.5f, 300f, 100f), string.Empty, button))
					{
						if (!eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieTank") == 1)
						bz("hunter", "Hunter", 15000);
					}
					if (gs("hunter") && !eliteZombie || eliteZombie && PlayerPrefs.GetInt("killedzombieTank") == 1)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6175f, 0.495f, 100f, 50f), "Hunter", font2);
					}
					else if (eliteZombie && PlayerPrefs.GetInt("killedzombieTank") == 0)
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6150f, 0.495f, 100f, 50f), "Kill Last", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.61f, 0.495f, 100f, 50f), "15000 RP", font2);
					}
				}
			}
			if (shopBools[1])
			{
				if (GUI.Button(Utilities.screenNoScaleRect(0.745f, 0.15f, 50f, 100f), string.Empty, arrowRight))
				{
					pbs();
					if (shopIndex[1] < 1)
					{
						shopIndex[1]++;
					}
				}
				if (GUI.Button(Utilities.screenNoScaleRect(0.715f, 0.15f, 50f, 100f), string.Empty, arrowLeft))
				{
					pbs();
					if (shopIndex[1] > 0)
					{
						shopIndex[1]--;
					}
				}
				if (shopIndex[1] == 0)
				{
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.2f, 300f, 100f), string.Empty, button))
					{
						pbs();
						if (myHuman != "Human")
						{
							doHuman("Human");
						}
					}
					GUI.Label(Utilities.screenNoScaleRect(0.455f, 0.195f, 100f, 50f), "Joe Blo", font2);
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.35f, 300f, 100f), string.Empty, button))
					{
						bh("nerd", "Nerd", 150);
					}
					if (gs("nerd"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.465f, 0.3425f, 100f, 50f), "Nerd", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.46f, 0.3425f, 100f, 50f), "150 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.5f, 300f, 100f), string.Empty, button))
					{
						bh("plumber", "Plumber", 500);
					}
					if (gs("plumber"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.455f, 0.4925f, 100f, 50f), "Worker", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.46f, 0.4925f, 100f, 50f), "500 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.65f, 300f, 100f), string.Empty, button))
					{
						bh("doctor", "Doctor", 1250);
					}
					if (gs("doctor"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.46f, 0.6425f, 100f, 50f), "Doctor", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.6425f, 100f, 50f), "1250 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.8f, 300f, 100f), string.Empty, button))
					{
						bh("cowbot", "Cowboy", 2500);
					}
					if (gs("cowbot"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.7925f, 100f, 50f), "Cowboy", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.7925f, 100f, 50f), "2500 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.2f, 300f, 100f), string.Empty, button))
					{
						bh("pastor", "Pastor", 3575);
					}
					if (gs("pastor"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6275f, 0.195f, 100f, 50f), "Priest", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6175f, 0.195f, 100f, 50f), "3575 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.35f, 300f, 100f), string.Empty, button))
					{
						bh("corsair", "Corsair", 4750);
					}
					if (gs("corsair"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6225f, 0.345f, 100f, 50f), "Drake", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6175f, 0.345f, 100f, 50f), "4750 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.5f, 300f, 100f), string.Empty, button))
					{
						bh("eskimo", "Eskimo", 6500);
					}
					if (gs("eskimo"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6175f, 0.495f, 100f, 50f), "Eskimo", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6175f, 0.495f, 100f, 50f), "6500 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.65f, 300f, 100f), string.Empty, button))
					{
						bh("marine", "Marine", 9000);
					}
					if (gs("marine"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6200f, 0.645f, 100f, 50f), "Marine", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.61f, 0.645f, 100f, 50f), "9000 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.8f, 300f, 100f), string.Empty, button))
					{
						bh("swat", "swat", 1200);
					}
					if (gs("swat"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6275f, 0.795f, 100f, 50f), "Swat", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.61f, 0.795f, 100f, 50f), "12000 RP", font2);
					}
				}
				if (shopIndex[1] == 1)
				{
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.2f, 300f, 100f), string.Empty, button))
					{
						bh("ninja", "Ninja", 13500);
					}
					if (gs("ninja"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.195f, 100f, 50f), "Kunoichi", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.195f, 100f, 50f), "13500 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.35f, 300f, 100f), string.Empty, button))
					{
						bh("beaf", "EnegyArmor", 15000);
					}
					if (gs("beaf"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.345f, 100f, 50f), "B.E.A.F.", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.345f, 100f, 50f), "15000 RP", font2);
					}
				}
			}
			if (shopBools[2])
			{
				if (GUI.Button(Utilities.screenNoScaleRect(0.745f, 0.15f, 50f, 100f), string.Empty, arrowRight))
				{
					pbs();
					if (shopIndex[2] < 1)
					{
						shopIndex[2]++;
					}
				}
				if (GUI.Button(Utilities.screenNoScaleRect(0.715f, 0.15f, 50f, 100f), string.Empty, arrowLeft))
				{
					pbs();
					if (shopIndex[2] > 0)
					{
						shopIndex[2]--;
					}
				}
				if (shopIndex[2] == 0)
				{
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.2f, 300f, 100f), string.Empty, button))
					{
						pbs();
						if (myGun != "MP5")
						{
							doGun("MP5");
						}
					}
					GUI.Label(Utilities.screenNoScaleRect(0.465f, 0.195f, 100f, 50f), "MP5", font2);
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.35f, 300f, 100f), string.Empty, button))
					{
						bw("winchester", "Wechester1200", 250);
					}
					if (gs("winchester"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.44f, 0.3425f, 100f, 50f), "Winchester", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.46f, 0.3425f, 100f, 50f), "250 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.5f, 300f, 100f), string.Empty, button))
					{
						bw("p90", "P90", 500);
					}
					if (gs("p90"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.47f, 0.4925f, 100f, 50f), "P90", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.46f, 0.4925f, 100f, 50f), "500 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.65f, 300f, 100f), string.Empty, button))
					{
						bw("m4", "M4", 2000);
					}
					if (gs("m4"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.4725f, 0.6425f, 100f, 50f), "M4", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.46f, 0.6425f, 100f, 50f), "2000 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.8f, 300f, 100f), string.Empty, button))
					{
						bw("remington", "Remington870", 3500);
					}
					if (gs("remington"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.4475f, 0.7925f, 100f, 50f), "Remington", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.7925f, 100f, 50f), "3500 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.2f, 300f, 100f), string.Empty, button))
					{
						bw("ak47", "AK47", 5000);
					}
					if (gs("ak47"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6175f, 0.195f, 100f, 50f), "AK47", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.61f, 0.195f, 100f, 50f), "5000 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.35f, 300f, 100f), string.Empty, button))
					{
						bw("aug", "AUG", 10000);
					}
					if (gs("aug"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6275f, 0.345f, 100f, 50f), "AUG", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.61f, 0.345f, 100f, 50f), "10000 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.5f, 300f, 100f), string.Empty, button))
					{
						bw("xm", "XM1014", 15000);
					}
					if (gs("xm"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6175f, 0.495f, 100f, 50f), "XM1014", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.61f, 0.495f, 100f, 50f), "15000 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.65f, 300f, 100f), string.Empty, button))
					{
						bw("gatling", "Gatlin", 20000);
					}
					if (gs("gatling"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.6175f, 0.645f, 100f, 50f), "Gatling", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.61f, 0.645f, 100f, 50f), "20000 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.57f, 0.8f, 300f, 100f), string.Empty, button))
					{
						bw("flamer", "FireGun", 22500);
					}
					if (gs("flamer"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.5905f, 0.795f, 100f, 50f), "FlameThrower", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.61f, 0.795f, 100f, 50f), "22500 RP", font2);
					}
				}
				if (shopIndex[2] == 1)
				{
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.2f, 300f, 100f), string.Empty, button))
					{
						bw("laser", "LaserGun", 35000);
					}
					if (gs("laser"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.4675f, 0.195f, 100f, 50f), "Laser", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.195f, 100f, 50f), "35000 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.35f, 300f, 100f), string.Empty, button))
					{
						bw("rpg", "RPG", 50000);
					}
					if (gs("rpg"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.4675f, 0.345f, 100f, 50f), "RPG", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.345f, 100f, 50f), "50000 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.5f, 300f, 100f), string.Empty, button))
					{
						bw("launcher", "M32", 80000);
					}
					if (gs("launcher"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.4675f, 0.495f, 100f, 50f), "M32", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.495f, 100f, 50f), "80000 RP", font2);
					}
					if (GUI.Button(Utilities.screenNoScaleRect(0.41f, 0.65f, 300f, 100f), string.Empty, button))
					{
						bw("pgm", "Missle", 120000);
					}
					if (gs("pgm"))
					{
						GUI.Label(Utilities.screenNoScaleRect(0.4675f, 0.645f, 100f, 50f), "PGM", font2);
					}
					else
					{
						GUI.Label(Utilities.screenNoScaleRect(0.45f, 0.645f, 100f, 50f), "120000 RP", font2);
					}
				}
			}
		}
	}

	public void DoTheShopBools(bool the, int index)
	{
		for (int i = 0; i < shopBools.Length; i++)
		{
			if (i == index)
			{
				shopBools[i] = the;
			}
			else
			{
				shopBools[i] = false;
			}
		}
	}

	public void DoTheSettingsBools(bool the, int index)
	{
		for (int i = 0; i < settingsBools.Length; i++)
		{
			if (i == index)
			{
				settingsBools[i] = the;
			}
			else
			{
				settingsBools[i] = false;
			}
		}
	}

	void OnApplicationQuit()
	{
		LevelIndex--;
		if (CurrentZombieHealth <= 0f)
		{
			CurrentZombieHealth = MaximumZombieHealth;
		}
	}

	public void LevelCycle()
	{
		if (LevelIndex >= 8)
		{
			LevelIndex = 0;
		}
		for (int i = 0; i < cons.Length; i++)
		{
			if (i != LevelIndex)
			{
				cons[i].gameObject.SetActive(false);
			}
			else
			{
				cons[i].gameObject.SetActive(true);
			}
		}
		MC.transform.parent.gameObject.transform.position = cons[LevelIndex].movingCamPos.position;
		LevelIndex++;
	}

	public void doFpsCam(bool b)
	{
		MC.guy.GetComponent<HumanStats>().fpsCam.enabled = b;
		MC.DetachedCam.enabled = !b;
	}

	public void doCamShake(float time)
	{
		if (MC.DetachedCam.GetComponent<CamShake>().shake)
		{
			return;
		}
		StartCoroutine(CamShake(time));
	}

	public IEnumerator CamShake(float time)
	{
		float f = 0f;
		MC.DetachedCam.GetComponent<CamShake>().shake = true;
		while (f < time)
		{
			yield return new WaitForEndOfFrame();
			f += Time.deltaTime;
		}
		MC.DetachedCam.GetComponent<CamShake>().shake = false;
	}

	public void doZombie(string str, bool start)
	{
		myZombie = str;
		Destroy(MC.zombie);
		if (eliteZombie)
		{
			str = "elite_" + str;
		}
		MC.zombie = Instantiate(Resources.Load<GameObject>("zombies/" + str), MC.gameObject.transform);
		RPSFromZombie = Resources.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().RPS;
		if (!start)
		{
			CurrentZombieHealth = Resources.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().health;
		}
		Game.ACInstance.PlayClip(Resources.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().sound, new Vector2(0.9f, 1.1f));
	}

	public void doMyZombie(bool start)
	{
		Destroy(MC.zombie);
		string str = myZombie;
		if (eliteZombie)
		{
			str = "elite_" + str;
		}
		MC.zombie = Instantiate(Resources.Load<GameObject>("zombies/" + str), MC.gameObject.transform);
		RPSFromZombie = Resources.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().RPS;
		if (!start)
		{
			CurrentZombieHealth = Resources.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().health;
		}
		Game.ACInstance.PlayClip(Resources.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().sound, new Vector2(0.9f, 1.1f));
	}

	public void doHuman(string str)
	{
		myHuman = str;
		Destroy(MC.guy);
		MC.guy = Instantiate(Resources.Load<GameObject>("guys/" + str), MC.gameObject.transform);
		doGun(myGun);
		RPSFromHuman = Resources.Load<GameObject>("guys/" + str).GetComponent<HumanStats>().rps;
		if (fpsCam)
		{
			doFpsCam(fpsCam);
		}
	}

	public void doGun(string str)
	{
		myGun = str;
		Destroy(MC.guy.GetComponent<HumanStats>().weapon);
		MC.guy.GetComponent<HumanStats>().weapon = Instantiate(Resources.Load<GameObject>("guns/" + str), MC.guy.GetComponent<HumanStats>().weaponPoint);
	}

	void Start()
	{
		if (LevelIndex < 0)
		{
			LevelIndex = 0;
		}
		if (LevelIndex > 8)
		{
			LevelIndex = 8;
		}
		if (PlayerPrefs.GetInt("donezombie") == 0)
		{
			myZombie = "Zombie";
			myHuman = "Human";
			myGun = "MP5";
			CurrentMusic = "BGM1";
			FOV = 60;
			doMyZombie(false);
			CurrentZombieHealth = Resources.Load<GameObject>("zombies/Zombie").GetComponent<ZombieStats>().health;
			PlayerPrefs.SetInt("donezombie", 1);
		}
		doMyZombie(true);
		doHuman(myHuman);
		doGun(myGun);
		audioCon.ChangeClip(CurrentMusic);
	}

	public void ZombieDeath()
	{
		if(MC.zombie.GetComponent<ZombieStats>().isDead)
		{
			return;
		}
		string str = (eliteZombie) ? "elite_" + myZombie : myZombie;
		RunPoints += Resources.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().deathPoints;
		Utilities.InsGobj(Resources.Load<GameObject>("pluspoints"), MC.plusPointsPos[UnityEngine.Random.Range(0, MC.plusPointsPos.Length)]).GetComponent<TextMesh>().text = "+" + MC.zombie.GetComponent<ZombieStats>().deathPoints;
		MC.zombie.transform.parent = deadZombieZone;
		MC.zombie.GetComponent<Animation>().Play("Death01");
		audioCon.PlayClip(MC.zombie.GetComponent<ZombieStats>().deathSound, new Vector2(0.9f, 1.1f));
		MC.zombie.GetComponent<ZombieStats>().isDead = true;
		if (eliteZombie && myZombie == "Hunter" && PlayerPrefs.GetInt("killedzombie" + myZombie) == 0)
		{
			Application.LoadLevel("claimPrize");
		}
		if (eliteZombie && PlayerPrefs.GetInt("killedzombie" + myZombie) == 0)
		{
			PlayerPrefs.SetInt("killedzombie" + myZombie, 1);
		}
	}

	public void ZombieAgain()
	{
		doMyZombie(false);
		CurrentZombieHealth = MaximumZombieHealth;
	}

	void Update()
	{
		#if UNITY_EDITOR
		if (Input.GetKeyDown("m"))
		{
			PlayerPrefs.DeleteAll();
		}
		if (Input.GetKeyDown("n"))
		{
			RunPoints += 10000;
		}
		if (Input.GetKeyDown("b"))
		{
			MC.DetachedCam.GetComponent<CamShake>().shake = !MC.DetachedCam.GetComponent<CamShake>().shake;
		}
		#endif
		if (CurrentZombieHealth == 0f)
		{
			ZombieDeath();
		}
		if (curCamera().fov != FOV)
		{
			curCamera().fov = FOV;
		}
		if (!MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().IsPlaying("RunShoot01") && !MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().IsPlaying("Run01") && !shotgun && !rpg && !MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().IsPlaying("RunShoot01_Laser"))
		{
			MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().Play("Run01");
		}
		if (MC.guy.GetComponent<HumanStats>().isLaser && !MC.guy.GetComponent<Animation>().IsPlaying("RunShoot01_Laser") && MC.guy.GetComponent<HumanStats>().weapon.GetComponent<AudioSource>().isPlaying)
		{
			MC.guy.GetComponent<HumanStats>().weapon.GetComponent<AudioSource>().Stop();
			foreach (ParticleRenderer rend in MC.guy.GetComponent<HumanStats>().weapon.GetComponent<WeaponStats>().emit)
			{
				rend.enabled = false;
			}
			if (MC.guy.GetComponent<HumanStats>().isFlamer)
			{
				foreach (ParticleEmitter emit2 in MC.guy.GetComponent<HumanStats>().weapon.GetComponent<WeaponStats>().emit2)
				{
					emit2.emit = false;
				}
			}
		}
		if (MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().IsPlaying("RunShoot01") && !MC.guy.GetComponent<HumanStats>().weapon.GetComponent<AudioSource>().isPlaying && !MC.guy.GetComponent<HumanStats>().isShotgun && !MC.guy.GetComponent<HumanStats>().isRPG || MC.guy.GetComponent<HumanStats>().GetComponent<Animation>().IsPlaying("RunShoot01_Laser") && !MC.guy.GetComponent<HumanStats>().weapon.GetComponent<AudioSource>().isPlaying)
		{
			MC.guy.GetComponent<HumanStats>().weapon.GetComponent<AudioSource>().Play();
		}
		else if (!MC.guy.GetComponent<Animation>().IsPlaying("RunShoot01") && MC.guy.GetComponent<HumanStats>().weapon.GetComponent<AudioSource>().isPlaying && !MC.guy.GetComponent<HumanStats>().isShotgun && !MC.guy.GetComponent<HumanStats>().isLaser && !MC.guy.GetComponent<HumanStats>().isRPG)
		{
			MC.guy.GetComponent<HumanStats>().weapon.GetComponent<AudioSource>().Stop();
		}
		RunPoints += RunPointsPerSecond * Time.deltaTime;
	}
}
