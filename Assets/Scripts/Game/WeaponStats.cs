using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float damage;
    
    public Vector2 randomDamage;
    
    public Transform firePoint;
    
    public Transform[] gatlingFirePoints;
    
    public ParticleRenderer[] emit;
    
    public ParticleEmitter[] emit2;
    
    public string rpgPrefab;

    public WeaponType weaponType;
    
    public enum WeaponType
    {
        normal = 0,
    
        gatling,
    
        shotgun,
    
        laser,
    
        flamer,
    
        pgm,
    
        rpg,
    
        launcher
    };
}
