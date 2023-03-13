using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour {
public float damage;
public Vector2 randomDamage;
public Transform firePoint;
public Transform[] gatlingFirePoints;
public ParticleRenderer[] emit;
public ParticleEmitter[] emit2;
public string rpgPrefab;
public enum WeaponType
{
    normal = 0,

    gatling = 1,

    shotgun = 2,

    laser = 3,

    flamer = 4,

    pgm = 5,

    rpg = 6,

    launcher = 7
};
public WeaponType weaponType;
}
