using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class RunController : MonoBehaviour
{
	public Transform deadZombieZone;

	public Container[] cons;
	
	public AudioController audioCon;

	public MovingCam MC;

	private bool shotgun;

	private bool rpg;

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
			return Storager.GetInt("totalunlockeditems");
		}
		set
		{
			Storager.SetInt("totalunlockeditems", value);
		}
	}

	public int FOV
	{
		get
		{
			return Storager.GetInt("fov");
		}
		set
		{
			Storager.SetInt("fov", value);
		}
	}

	public string myTrueZombie
	{
		get
		{
			return Storager.GetString("myzombie");
		}
	}

	public string myZombie
	{
		get
		{
			return Storager.GetString("myzombie");
		}
		set
		{
			Storager.SetString("myzombie", value);
		}
	}

	public bool eliteZombie
	{
		get
		{
			return Storager.GetInt("shopelitezombie") == 1;
		}
		set
		{
			if (!value)
			{
				Storager.SetInt("shopelitezombie", 0);
				return;
			}
			Storager.SetInt("shopelitezombie", 1);
		}
	}

	public string myHuman
	{
		get
		{
			return Storager.GetString("myhuman");
		}
		set
		{
			Storager.SetString("myhuman", value);
		}
	}

	public string myGun
	{
		get
		{
			return Storager.GetString("mygun");
		}
		set
		{
			Storager.SetString("mygun", value);
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
	
	public bool[] settingsBools =
	{
		false,
		false,
		false,
		false,
		false
	};

	public Camera curCamera()
	{
		if (Storager.GetInt("fpscam") == 0)
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
		return Storager.GetInt("shop" + s) == 1;
	}

	public void ss(string s, int i)
	{
		Storager.SetInt("shop" + s, i);
	}

	public float RunPoints
	{
		get
		{
			return Storager.GetFloat("runpoints");
		}
		set
		{
			Storager.SetFloat("runpoints", value);
		}
	}

	public float RunPointsPerSecond
	{
		get
		{
			return Storager.GetFloat("rps") + RPSFromZombie + RPSFromHuman;
		}
		set
		{
			Storager.SetFloat("rps", value + RPSFromZombie + RPSFromHuman);
		}
	}

	public float RPSFromZombie
	{
		get
		{
			return Storager.GetFloat("rpszombie");
		}
		set
		{
			Storager.SetFloat("rpszombie", value);
		}
	}

	public float RPSFromHuman
	{
		get
		{
			return Storager.GetFloat("rpshuman");
		}
		set
		{
			Storager.SetFloat("rpshuman", value);
		}
	}

	public string CurrentMusic
	{
		get
		{
			return Storager.GetString("currentmusic");
		}
		set
		{
			Storager.SetString("currentmusic", value);
		}
	}

	public bool fpsCam
	{
		get
		{
			return Storager.GetInt("fpscam") == 1;
		}
		set
		{
			if (!value)
			{
				Storager.SetInt("fpscam", 0);
				return;
			}
			Storager.SetInt("fpscam", 1);
		}
	}

	public int LevelIndex
	{
		get
		{
			return Storager.GetInt("levelindex");
		}
		set
		{
			Storager.SetInt("levelindex", value);
		}
	}

	public float CurrentZombieHealth
	{
		get
		{
			return Storager.GetFloat("zombiehealth");
		}
		set
		{
			Storager.SetFloat("zombiehealth", value);
		}
	}

	public float MaximumZombieHealth
	{
		get
		{
			string str = (eliteZombie) ? "elite_" + myZombie : myZombie;
			return GameAsset.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().health;
		}
	}

	public float CurrentZombieTime
	{
		get
		{
			return Storager.GetFloat("zombietime");
		}
		set
		{
			Storager.SetFloat("zombietime", value);
		}
	}

	public float MaximumZombieTime
	{
		get
		{
			string str = (eliteZombie) ? "elite_" + myZombie : myZombie;
			return GameAsset.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().respawnTime;
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

	public GameObject SpawnPlusPoints()
	{
		GameObject obj = Utilities.InsGobj(GameAsset.Load<GameObject>("UITextPopup"), Game.RUCInstance.UILayers[0].transform);
		obj.transform.localPosition = new Vector3(UnityEngine.Random.Range(-27f, 50f), UnityEngine.Random.Range(-12f, -26f), -3.6f);
		return obj;
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
			RunPoints += GameAsset.Load<GameObject>("guns/" + myGun).GetComponent<WeaponStats>().damage;
			SpawnPlusPoints().GetComponentInChildren<TextMesh>().text = "+" + MC.guy.GetComponent<HumanStats>().weapon.GetComponent<WeaponStats>().damage;
			if (CurrentZombieHealth > 0f)
			{
				CurrentZombieHealth -= GameAsset.Load<GameObject>("guns/" + myGun).GetComponent<WeaponStats>().damage;
			}
			if (CurrentZombieHealth < 0f)
			{
				CurrentZombieHealth = 0f;
			}
		}
		else
		{
			float the = UnityEngine.Random.Range(GameAsset.Load<GameObject>("guns/" + myGun).GetComponent<WeaponStats>().randomDamage.x, GameAsset.Load<GameObject>("guns/" + myGun).GetComponent<WeaponStats>().randomDamage.y);
			SpawnPlusPoints().GetComponentInChildren<TextMesh>().text = "+" + Math.Round(the);
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
			return;
		}
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
			return;
		}
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
			return;
		}
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
			return;
		}
	}

	public void ChangeMusic(string music)
	{
		if (CurrentMusic != music)
		{
			CurrentMusic = music;
			audioCon.ChangeClip(CurrentMusic);
		}
	}

	public void DoAPopup(string popupText, Vector2 textPosition)
	{
		Game.RUCInstance.UILayers[2].SetActive(true);
		Game.RUCInstance.GetLabel("PopupUIText").transform.localPosition = new Vector3(textPosition.x, textPosition.y, -12.37f);
		Game.RUCInstance.GetLabel("PopupUIText").text = popupText;
	}

	public void DoAYesNoPopup(string popupText, Vector2 textPosition, string yesFunction)
	{
		currentYesFunction = yesFunction;
		Game.RUCInstance.UILayers[4].SetActive(true);
		Game.RUCInstance.GetLabel("PopupYesNoUIText").transform.localPosition = new Vector3(textPosition.x, textPosition.y, -12.37f);
		Game.RUCInstance.GetLabel("PopupYesNoUIText").text = popupText;
	}

	public void LoadBackToMenu()
	{
		foreach(AudioSource AS in FindObjectsOfTypeAll(typeof(AudioSource)))
		{
			if (AS.name != "LoadingUI" && AS.gameObject.activeInHierarchy)
			{
				AS.enabled = false;
			}
		}
		LoadingUI.LoadLevel("menu");
		Game.RUCInstance.UILayers[0].transform.parent.parent.gameObject.SetActive(false);
	}

	private string currentYesFunction;

	public void SlidersGroup1(IdleThreeDeeSlider.SliderValue sliderValue)
	{
		switch (sliderValue.name)
		{
			case "VolumeSlider":
			Global.currentVolume = sliderValue.value;
			Game.RUCInstance.GetLabel("VolumeAmountText").text = "" + Math.Round(Global.currentVolume);
			break;
		}
	}

	public void ButtonsGroup2(string ButtonName)
	{
		if (Game.RUCInstance.UILayers[2].activeInHierarchy && ButtonName != "PopupButton_OK" || Game.RUCInstance.UILayers[4].activeInHierarchy && ButtonName != "PopupButton_Yes" && ButtonName != "PopupButton_No")
		{
			return;
		}
		switch (ButtonName)
		{
			
		}
	}

	public void ButtonsGroup1(string ButtonName)
	{
		if (Game.RUCInstance.UILayers[2].activeInHierarchy && ButtonName != "PopupButton_OK" || Game.RUCInstance.UILayers[4].activeInHierarchy && ButtonName != "PopupButton_Yes" && ButtonName != "PopupButton_No")
		{
			return;
		}
		switch (ButtonName)
		{
			case"PopupButton_OK":
			Game.RUCInstance.UILayers[2].SetActive(false);
			break;
			case"PopupButton_Yes":
			this.SendMessage(currentYesFunction);
			break;
			case"PopupButton_No":
			Game.RUCInstance.UILayers[4].SetActive(false);
			break;
			case"BackToMenu":
			DoAYesNoPopup("Would you like to return to the menu?", new Vector2(-0.9f, -19.4f), "LoadBackToMenu");
			break;
			case"Settings":
			Game.RUCInstance.UILayers[3].SetActive(true);
			Game.RUCInstance.UILayers[0].SetActive(false);
			break;
			case"SettingsButton_Back":
			Game.RUCInstance.UILayers[0].SetActive(true);
			Game.RUCInstance.UILayers[3].SetActive(false);
			break;
			case"SettingsButton_Music":
			foreach (GameObject obj in Game.RUCInstance.SettingsUILayers)
			{
				if (obj.name == "MusicTab")
				{
					obj.SetActive(true);
				}
				else
				{
					obj.SetActive(false);
				}
			}
			break;
			case"SettingsButton_Video":
			foreach (GameObject obj in Game.RUCInstance.SettingsUILayers)
			{
				if (obj.name == "VideoTab")
				{
					obj.SetActive(true);
				}
				else
				{
					obj.SetActive(false);
				}
			}
			break;
			case"SettingsButton_Audio":
			foreach (GameObject obj in Game.RUCInstance.SettingsUILayers)
			{
				if (obj.name == "AudioTab")
				{
					obj.SetActive(true);
				}
				else
				{
					obj.SetActive(false);
				}
			}
			break;
			case"SettingsButton_General":
			foreach (GameObject obj in Game.RUCInstance.SettingsUILayers)
			{
				if (obj.name == "GeneralTab")
				{
					obj.SetActive(true);
				}
				else
				{
					obj.SetActive(false);
				}
			}
			break;
			case"GeneralButton_Credits":
			LoadingUI.LoadLevel("credits");
			Game.RUCInstance.UILayers[0].transform.parent.parent.gameObject.SetActive(false);
			break;
			case"MusicButton_COMZBGM1":
			ChangeMusic("BGM1");
			break;
			case"MusicButton_COMZBGM2":
			ChangeMusic("BGM2");
			break;
			case"MusicButton_MillerInst":
			ChangeMusic("MILLERINST");
			break;
			case"MusicButton_AlphaMenu":
			ChangeMusic("AlphaMenu");
			break;
			case"MusicButton_BetaMenu":
			ChangeMusic("BetaMenu");
			break;
			case"MusicButton_DCBoss":
			ChangeMusic("DinoCapBoss");
			break;
			case"MusicButton_AIMusic":
			ChangeMusic("aigeneratedsong");
			break;
			case"SettingsButton_FirstPersonCam":
			fpsCam = !fpsCam;
			doFpsCam(fpsCam);
			break;
			case"SettingsButton_FOVUp":
			if (FOV < 90)
				FOV++;
			break;
			case"SettingsButton_FOVDown":
			if (FOV > 45)
				FOV--;
			break;
			case"SettingsButton_Mute":
			Global.mute = !Global.mute;
			if (Global.mute)
			{
				Game.ACInstance.GetComponent<AudioSource>().clip = null;
			}
			else
			{
				audioCon.ChangeClip(CurrentMusic);
			}
			break;
			case"SettingsButton_MuteMusic":
			Global.muteMusic = !Global.muteMusic;
			if (Global.muteMusic)
			{
				Game.ACInstance.GetComponent<AudioSource>().clip = null;
			}
			else
			{
				audioCon.ChangeClip(CurrentMusic);
			}
			break;
			case"ShopButton_Prestige":
			foreach (GameObject obj in Game.RUCInstance.ShopUILayers)
			{
				if (obj.name == "PrestigeShop")
				{
					obj.SetActive(true);
				}
				else
				{
					obj.SetActive(false);
				}
			}
			break;
			case"ShopButton_Upgrades":
			foreach (GameObject obj in Game.RUCInstance.ShopUILayers)
			{
				if (obj.name == "UpgradeShop")
				{
					obj.SetActive(true);
				}
				else
				{
					obj.SetActive(false);
				}
			}
			break;
			case"ShopButton_PrestigeButton":
			if (Storager.GetInt("prestigeTokenLevel1") == 0)
			DoAPopup("you do not meet the requirements\nto prestige at this current time.", new Vector2(1.23f, -18.24f));
			else
			DoAPopup("you do meet the requirements\nto prestige at this current time, too bad though", new Vector2(1.23f, -18.24f));
			break;
			case"ShopButton_EliteZombie":
			bc("elitezombie", 100000);
			if (gs("elitezombie") && Storager.GetInt("you did the moment") == 0 || gs("elitezombie") && Storager.GetInt("you did the moment") == 1 && myZombie != "Zombie")
			{
				Storager.SetInt("you did the moment", 1);
				doZombie("Zombie", false);
			}
			break;
			case"ShopButton_Joeblo":
			if (myHuman != "Human")
			{
				doHuman("Human");
			}
			break;
			case"ShopButton_Nerd":
			bh("nerd", "Nerd", 150);
			break;
			case"ShopButton_Worker":
			bh("plumber", "Plumber", 500);
			break;
			case"ShopButton_Doctor":
			bh("doctor", "Doctor", 1250);
			break;
			case"ShopButton_Cowboy":
			bh("cowbot", "Cowboy", 2500);
			break;
			case"ShopButton_Priest":
			bh("pastor", "Pastor", 3575);
			break;
			case"ShopButton_Drake":
			bh("corsair", "Corsair", 4750);
			break;
			case"ShopButton_Eskimo":
			bh("eskimo", "Eskimo", 6500);
			break;
			case"ShopButton_Marine":
			bh("marine", "Marine", 9000);
			break;
			case"ShopButton_Swat":
			bh("swat", "swat", 12000);
			break;
			case"ShopButton_Kunoichi":
			bh("ninja", "Ninja", 13500);
			break;
			case"ShopButton_B.E.A.F":
			bh("beaf", "EnegyArmor", 15000);
			break;
			case"ShopButton_MP5":
			if (myGun != "MP5")
			{
				doGun("MP5");
			}
			break;
			case"ShopButton_Winchester":
			bw("winchester", "Wechester1200", 250);
			break;
			case"ShopButton_P90":
			bw("p90", "P90", 500);
			break;
			case"ShopButton_M4":
			bw("m4", "M4", 2000);
			break;
			case"ShopButton_Remington":
			bw("remington", "Remington870", 3500);
			break;
			case"ShopButton_AK47":
			bw("ak47", "AK47", 5000);
			break;
			case"ShopButton_AUG":
			bw("aug", "AUG", 10000);
			break;
			case"ShopButton_XM1014":
			bw("xm", "XM1014", 15000);
			break;
			case"ShopButton_Gatling":
			bw("gatling", "Gatlin", 20000);
			break;
			case"ShopButton_FlameThrower":
			bw("flamer", "FireGun", 22500);
			break;
			case"ShopButton_Laser":
			bw("laser", "LaserGun", 35000);
			break;
			case"ShopButton_RPG":
			bw("rpg", "RPG", 50000);
			break;
			case"ShopButton_M32":
			bw("launcher", "M32", 80000);
			break;
			case"ShopButton_PGM":
			bw("pgm", "Missle", 120000);
			break;
			case"Shop":
			Game.RUCInstance.UILayers[1].SetActive(true);
			Game.RUCInstance.UILayers[0].SetActive(false);
			break;
			case"ShopButton_Back":
			Game.RUCInstance.UILayers[0].SetActive(true);
			Game.RUCInstance.UILayers[1].SetActive(false);
			break;
			case"ShopButton_Zombies":
			foreach (GameObject obj in Game.RUCInstance.ShopUILayers)
			{
				if (obj.name == "ZombieShop")
				{
					obj.SetActive(true);
				}
				else
				{
					obj.SetActive(false);
				}
			}
			break;
			case"ShopButton_Avatars":
			foreach (GameObject obj in Game.RUCInstance.ShopUILayers)
			{
				if (obj.name == "AvatarShop")
				{
					obj.SetActive(true);
				}
				else
				{
					obj.SetActive(false);
				}
			}
			break;
			case"ShopButton_Weapons":
			foreach (GameObject obj in Game.RUCInstance.ShopUILayers)
			{
				if (obj.name == "WeaponShop")
				{
					obj.SetActive(true);
				}
				else
				{
					obj.SetActive(false);
				}
			}
			break;
			case"Shoot":
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
			break;
			case"ShopButton_Zombie":
			if (myZombie != "Zombie")
			{
				doZombie("Zombie", false);
			}
			break;
			case"ShopButton_Dog":
			if (!eliteZombie || eliteZombie && Storager.GetInt("killedzombieZombie") == 1)
			bz("dog", "Dog", 250);
			else
			DoAPopup("You must first kill the elite zombie.", new Vector2(1.15f, -19.44f));
			break;
			case"ShopButton_Nurse":
			if (!eliteZombie || eliteZombie && Storager.GetInt("killedzombieDog") == 1)
			bz("nurse", "Nurse", 750);
			else
			DoAPopup("You must first kill the elite dog.", new Vector2(1.6f, -19.44f));
			break;
			case"ShopButton_ZombiePolice":
			if (!eliteZombie || eliteZombie && Storager.GetInt("killedzombieNurse") == 1)
			bz("policezombie", "Zombie_Police", 1500);
			else
			DoAPopup("You must first kill the elite nurse.", new Vector2(1.2f, -19.44f));
			break;
			case"ShopButton_ZombieSwat":
			if (!eliteZombie || eliteZombie && Storager.GetInt("killedzombieZombie_Police") == 1)
			bz("swatzombie", "Zombie_Swat", 2500);
			else
			DoAPopup("You must first kill the\nelite police zombie.", new Vector2(3.9f, -19f));
			break;
			case"ShopButton_Boomer":
			if (!eliteZombie || eliteZombie && Storager.GetInt("killedzombieZombie_Swat") == 1)
			bz("boomer", "Boomer", 4000);
			else
			DoAPopup("You must first kill the\nelite swat zombie.", new Vector2(3.9f, -19f));
			break;
			case"ShopButton_Tank":
			if (!eliteZombie || eliteZombie && Storager.GetInt("killedzombieBoomer") == 1)
			bz("tank", "Tank", 7500);
			else
			DoAPopup("You must first kill the elite boomer.", new Vector2(0.84f, -19.44f));
			break;
			case"ShopButton_Hunter":
			if (!eliteZombie || eliteZombie && Storager.GetInt("killedzombieTank") == 1)
			bz("hunter", "Hunter", 15000);
			else
			DoAPopup("You must first kill the elite tank.", new Vector2(1.8f, -19.44f));
			break;
			default:
			Debug.LogError("found no button function for " + ButtonName);
			break;
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
		if (MC.camShake.GetComponent<CamShake>().shake)
		{
			return;
		}
		StartCoroutine(CamShake(time));
	}

	public IEnumerator CamShake(float time)
	{
		float f = 0f;
		MC.camShake.GetComponent<CamShake>().shake = true;
		while (f < time)
		{
			yield return new WaitForEndOfFrame();
			f += Time.deltaTime;
		}
		MC.camShake.GetComponent<CamShake>().shake = false;
	}

	public void doZombie(string str, bool start)
	{
		myZombie = str;
		Destroy(MC.zombie);
		if (eliteZombie)
		{
			str = "elite_" + str;
		}
		MC.zombie = Instantiate(GameAsset.Load<GameObject>("zombies/" + str), MC.gameObject.transform);
		RPSFromZombie = GameAsset.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().RPS;
		if (!start)
		{
			CurrentZombieHealth = GameAsset.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().health;
		}
		Game.ACInstance.PlayClip(GameAsset.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().sound, new Vector2(0.9f, 1.1f));
	}

	public void doMyZombie(bool start)
	{
		Destroy(MC.zombie);
		string str = myZombie;
		if (eliteZombie)
		{
			str = "elite_" + str;
		}
		MC.zombie = Instantiate(GameAsset.Load<GameObject>("zombies/" + str), MC.gameObject.transform);
		RPSFromZombie = GameAsset.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().RPS;
		if (!start)
		{
			CurrentZombieHealth = GameAsset.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().health;
		}
		Game.ACInstance.PlayClip(GameAsset.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().sound, new Vector2(0.9f, 1.1f));
	}

	public void doHuman(string str)
	{
		myHuman = str;
		Destroy(MC.guy);
		MC.guy = Instantiate(GameAsset.Load<GameObject>("guys/" + str), MC.gameObject.transform);
		doGun(myGun);
		RPSFromHuman = GameAsset.Load<GameObject>("guys/" + str).GetComponent<HumanStats>().rps;
		if (fpsCam)
		{
			doFpsCam(fpsCam);
		}
	}

	public void doGun(string str)
	{
		myGun = str;
		Destroy(MC.guy.GetComponent<HumanStats>().weapon);
		MC.guy.GetComponent<HumanStats>().weapon = Instantiate(GameAsset.Load<GameObject>("guns/" + str), MC.guy.GetComponent<HumanStats>().weaponPoint);
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
		if (Storager.GetInt("donezombie") == 0)
		{
			myZombie = "Zombie";
			myHuman = "Human";
			myGun = "MP5";
			CurrentMusic = "BGM1";
			FOV = 60;
			doMyZombie(false);
			CurrentZombieHealth = GameAsset.Load<GameObject>("zombies/Zombie").GetComponent<ZombieStats>().health;
			Storager.SetInt("donezombie", 1);
		}
		doMyZombie(true);
		doHuman(myHuman);
		doGun(myGun);
		audioCon.ChangeClip(CurrentMusic);
		Game.RUCInstance.GetLabel("VolumeAmountText").text = "" + Math.Round(Global.currentVolume);
	}

	public void ZombieDeath()
	{
		if(MC.zombie.GetComponent<ZombieStats>().isDead)
		{
			return;
		}
		string str = (eliteZombie) ? "elite_" + myZombie : myZombie;
		RunPoints += GameAsset.Load<GameObject>("zombies/" + str).GetComponent<ZombieStats>().deathPoints;
		SpawnPlusPoints().GetComponentInChildren<TextMesh>().text = "+" + MC.zombie.GetComponent<ZombieStats>().deathPoints;
		MC.zombie.transform.parent = deadZombieZone;
		MC.zombie.GetComponent<Animation>().Play("Death01");
		audioCon.PlayClip(MC.zombie.GetComponent<ZombieStats>().deathSound, new Vector2(0.9f, 1.1f));
		MC.zombie.GetComponent<ZombieStats>().isDead = true;
		if (eliteZombie && myZombie == "Hunter" && Storager.GetInt("killedzombie" + myZombie) == 0)
		{
			DoAPopup("you got a prestige token!", new Vector2(4.22f, -19.44f));
			Storager.SetInt("pretigeTokenLevel1", 1);
		}
		if (eliteZombie && Storager.GetInt("killedzombie" + myZombie) == 0)
		{
			Storager.SetInt("killedzombie" + myZombie, 1);
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
			Storager.RemoveAll();
		}
		if (Input.GetKeyDown("n"))
		{
			RunPoints += 10000;
		}
		if (Input.GetKeyDown("b"))
		{
			MC.camShake.GetComponent<CamShake>().shake = !MC.camShake.GetComponent<CamShake>().shake;
		}
		#endif
		if (GameObject.Find("Laser(Clone)") && MC.guy.GetComponent<HumanStats>().isLaser && !MC.guy.GetComponent<Animation>().IsPlaying("RunShoot01_Laser"))
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
		Game.RUCInstance.EnemyHealthBar.transform.localScale = new Vector3(Utilities.Percentage(CurrentZombieHealth, MaximumZombieHealth) / 100f, 1f, 1f);
		Game.RUCInstance.GetLabel("RPLabel").text = "" + Math.Round(RunPoints);
		Game.RUCInstance.GetLabel("RPLabelShop").text = "" + Math.Round(RunPoints);
		Game.RUCInstance.GetLabel("RPSLabel").text = "" + RunPointsPerSecond;
		Game.RUCInstance.GetLabel("FOVAmountText").text = "" + FOV;
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
			if (!Global.mute)
			{
				MC.guy.GetComponent<HumanStats>().weapon.GetComponent<AudioSource>().Play();
			}
		}
		else if (!MC.guy.GetComponent<Animation>().IsPlaying("RunShoot01") && MC.guy.GetComponent<HumanStats>().weapon.GetComponent<AudioSource>().isPlaying && !MC.guy.GetComponent<HumanStats>().isShotgun && !MC.guy.GetComponent<HumanStats>().isLaser && !MC.guy.GetComponent<HumanStats>().isRPG)
		{
			MC.guy.GetComponent<HumanStats>().weapon.GetComponent<AudioSource>().Stop();
		}
		RunPoints += RunPointsPerSecond * Time.deltaTime;
	}
}
