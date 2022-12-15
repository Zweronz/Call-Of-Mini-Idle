using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class HumanStats : MonoBehaviour {
public float rps;
public Transform weaponPoint;
public GameObject weapon;
public Camera fpsCam;
public bool isLaser
{
    get
    {
        return weapon.GetComponent<WeaponStats>().weaponType == WeaponStats.WeaponType.laser || weapon.GetComponent<WeaponStats>().weaponType == WeaponStats.WeaponType.flamer;
    }
}
public bool isShotgun
{
    get
    {
        return weapon.GetComponent<WeaponStats>().weaponType == WeaponStats.WeaponType.shotgun;
    }
}
public bool isFlamer
{
    get
    {
        return weapon.GetComponent<WeaponStats>().weaponType == WeaponStats.WeaponType.flamer;
    }
}
public bool isRPG
{
    get
    {
        return weapon.GetComponent<WeaponStats>().weaponType == WeaponStats.WeaponType.rpg;
    }
}
public IEnumerator doLaser()
{
	yield return new WaitForSeconds(0.03f);
    if (!GameObject.Find("Laser(Clone)"))
    {
        GameObject objthe = Utilities.InsGobj(Resources.Load<GameObject>("Laser"), weapon.GetComponent<WeaponStats>().firePoint.position, Quaternion.identity);
        objthe.transform.Rotate(0f, 180f, 0f);
        objthe.transform.parent = weaponPoint;
        Game.RCInstance.EffectsCache.Add(objthe);
    }
}
public void DoShot()
{
    if(!isRPG)
    Game.RCInstance.Shoot();
    switch(weapon.GetComponent<WeaponStats>().weaponType)
    {
        case WeaponStats.WeaponType.rpg:
        GameObject objthe2 = Utilities.InsGobj(Resources.Load<GameObject>(weapon.GetComponent<WeaponStats>().rpgPrefab), weapon.GetComponent<WeaponStats>().firePoint.position, Quaternion.identity);
        objthe2.transform.Rotate(new Vector3 (0f, 180f, 0f));
        Game.RCInstance.EffectsCache.Add(objthe2);
        Game.ACInstance.PlayClip(weapon.GetComponent<AudioSource>().clip, new Vector2(0.99f, 1.01f));
        break;

        case WeaponStats.WeaponType.flamer:
        break;

        case WeaponStats.WeaponType.laser:
        StartCoroutine(doLaser());
        break;
        
        case WeaponStats.WeaponType.shotgun:
        Game.RCInstance.EffectsCache.Add(Utilities.InsGobj(Resources.Load<GameObject>("Sparks"), weapon.GetComponent<WeaponStats>().firePoint.position, Quaternion.identity));
        GameObject objthe = Utilities.InsGobj(Resources.Load<GameObject>("ShotGunFire"), weapon.GetComponent<WeaponStats>().firePoint.position, Quaternion.identity);
        objthe.transform.Rotate(0f, 180f, 0f);
        Game.RCInstance.EffectsCache.Add(objthe);
        Game.ACInstance.PlayClip(weapon.GetComponent<AudioSource>().clip, new Vector2(0.99f, 1.01f));
        break;

        case WeaponStats.WeaponType.gatling:
        for (int i = 0; i < weapon.GetComponent<WeaponStats>().gatlingFirePoints.Length; i++)
        {
            GameObject obj3 = Utilities.InsGobj(Resources.Load<GameObject>("Sparks"), weapon.GetComponent<WeaponStats>().gatlingFirePoints[0].position, Quaternion.identity);
            obj3.transform.parent = this.transform;
            Game.RCInstance.EffectsCache.Add(obj3);
            GameObject obj2 = Utilities.InsGobj(Resources.Load<GameObject>("FireLine"), weapon.GetComponent<WeaponStats>().gatlingFirePoints[i].position, Quaternion.identity);
            obj2.transform.Rotate(new Vector3 (0f, 180f, 80f));
            Game.RCInstance.EffectsCache.Add(obj2);
        }
        break;

        case WeaponStats.WeaponType.normal:
        Game.RCInstance.EffectsCache.Add(Utilities.InsGobj(Resources.Load<GameObject>("Sparks"), weapon.GetComponent<WeaponStats>().firePoint.position, Quaternion.identity));
        GameObject obj = Utilities.InsGobj(Resources.Load<GameObject>("FireLine"), weapon.GetComponent<WeaponStats>().firePoint.position, Quaternion.identity);
        obj.transform.Rotate(new Vector3 (0f, 180f, 80f));
        Game.RCInstance.EffectsCache.Add(obj);
        break;
    }
}
}
