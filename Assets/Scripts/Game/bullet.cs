using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class bullet : MonoBehaviour
{
    public float bulletSpeed = 300f;

    public float lifetime = 5f;

    public Transform tip;

    public enum BulletType
    {
        normal = 0,

        rocket = 1
    };

    public BulletType bulletType;

    void Start()
    {
        StartCoroutine(RemoveSelf());
    }

    void Update()
    {
        base.transform.position += base.transform.forward * bulletSpeed * Time.deltaTime;
        if(Physics.CheckSphere(tip.position, 0.01f))
        {
            switch(bulletType)
            {
                case BulletType.normal:
                RunController.instance.EffectsCache.Add(Utilities.InsGobj(GameAsset.Load<GameObject>("Sparks"), tip.position, Quaternion.identity));
                RemoveSelfNoWait();
                break;

                case BulletType.rocket:
                RunController.instance.EffectsCache.Add(Utilities.InsGobj(GameAsset.Load<GameObject>("RPG_EXP"), tip.position, Quaternion.identity));
                RunController.instance.doCamShake(0.1f);
                RunController.instance.Shoot();
                RemoveSelfNoWait();
                break;
            }
        }
    }

    public void RemoveSelfNoWait()
    {
        try
	    {
	    	RunController.instance.EffectsCache.RemoveAt(RunController.instance.EffectsCache.IndexOf(base.gameObject));
	    }
	    catch
	    {
	    	Debug.Log("unfound");
	    }
        Destroy(gameObject);
    }

    public IEnumerator RemoveSelf()
    {
        yield return new WaitForSeconds(lifetime);
        RemoveSelfNoWait();
    }
}
