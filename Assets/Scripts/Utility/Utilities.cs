using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour 
{

public static Vector2 MouseVec
{
	get
	{
		return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
	}
}

public static bool IfRandom(int oneInWhat)
{
	return UnityEngine.Random.Range(0, oneInWhat + 1) == oneInWhat;
}

public static Rect screenScaleRect(float left, float top, float width, float height)
{
	return new Rect((float)Screen.width * left, (float)Screen.height * top, (float)Screen.width * width, (float)Screen.height * height);
}

public static Rect screenNoScaleRect(float left, float top, float width, float height)
{
	return new Rect((float)Screen.width * left, (float)Screen.height * top, width, height);
}

public static GameObject LoadObject(string str)
{
	return GameAsset.Load(str) as GameObject;
}

public static AnimationState GetAnimState(Animation anim, string str)
{
	foreach (AnimationState AS in anim)
	{
		if (AS.name == str)
		{
			return AS;
		}
	}
	return null;
}

public static GameObject InsGobj(Object original)
{
	GameObject gobj = Instantiate(original) as GameObject;
	gobj.name.Replace("(Clone)", string.Empty);
	return gobj;
}

public static GameObject InsGobj(Object original, Transform parent)
{
	GameObject gobj = Instantiate(original, parent) as GameObject;
	gobj.name.Replace("(Clone)", string.Empty);
	return gobj;
}

public static GameObject InsGobj(Object original, Transform parent, bool instantiateInWorldSpace)
{
	GameObject gobj = Instantiate(original, parent, instantiateInWorldSpace) as GameObject;
	gobj.name.Replace("(Clone)", string.Empty);
	return gobj;
}

public static GameObject InsGobj(Object original, Vector3 position, Quaternion rotation)
{
	GameObject gobj = Instantiate(original, position, rotation) as GameObject;
	gobj.name.Replace("(Clone)", string.Empty);
	return gobj;
}

public static GameObject InsGobj(Object original, Vector3 position, Quaternion rotation, Transform parent)
{
	GameObject gobj = Instantiate(original, position, rotation, parent) as GameObject;
	gobj.name.Replace("(Clone)", string.Empty);
	return gobj;
}

public static float Percentage(float current, float maximum)
{
	return current / maximum * 100;
}

}
