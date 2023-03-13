using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class MovingCam : MonoBehaviour
{
    public GameObject guy;
    
    public GameObject zombie;
    
    public Camera DetachedCam;
    
    public Transform[] plusPointsPos;
    
    public void Cycle()
    {
        Game.RCInstance.LevelCycle();
        Game.RCInstance.ClearEffectsCache();
    }
}
