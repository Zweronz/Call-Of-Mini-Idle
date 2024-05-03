using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class ZombieStats : MonoBehaviour
{
    public float RPS;

    public AudioClip sound;

    public float health;

    [HideInInspector] public bool walkedUp;

    public string walkAnimName;

    public float deathPoints;

    [HideInInspector] public bool isDead;

    public float respawnTime;

    public AudioClip deathSound;

    void Start()
    {
        GetComponent<Animation>().Play("WalkUp");
    }
    
    void Update()
    {
        if (isDead && !GetComponent<Animation>().IsPlaying("Death01"))
        {
            RunController.instance.ZombieAgain();
        }
        if (!walkedUp && !GetComponent<Animation>().IsPlaying("WalkUp"))
        {
            walkedUp = true;
        }
        if (walkedUp && !isDead && !GetComponent<Animation>().isPlaying)
        {
            GetComponent<Animation>().Play(walkAnimName);
        }
    }
}
