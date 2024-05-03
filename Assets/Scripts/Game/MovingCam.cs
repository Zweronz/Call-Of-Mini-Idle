using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class MovingCam : MonoBehaviour
{
    public GameObject guy;
    
    public GameObject zombie;
    
    public Camera DetachedCam;

    public Transform camShake;
    
    public Transform[] plusPointsPos;

    private Vector3 startingPosition;
    
    public void Cycle()
    {
        RunController.instance.LevelCycle();
        RunController.instance.ClearEffectsCache();
    }
}
