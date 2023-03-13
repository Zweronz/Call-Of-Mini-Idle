using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameAsset
{
	public static T Load<T>(string path) where T : UnityEngine.Object
	{
		CheckInit();
	    return CachedAssets.Find(x => x.assetPath == path).assetObject as T;
	}

	public static T[] LoadFolder<T>(string path) where T : UnityEngine.Object
	{
		CheckInit();
	    return (from asset in CachedAssets where asset.assetPath.StartsWith(path) select asset.assetObject as T).ToArray();
	}

	public static UnityEngine.Object Load(string path)
	{
		CheckInit();
	    return CachedAssets.Find(x => x.assetPath == path).assetObject;
	}

	public static UnityEngine.Object[] LoadFolder(string path)
	{
		CheckInit();
	    return (from asset in CachedAssets where asset.assetPath.StartsWith(path) select asset.assetObject).ToArray();
	}

	public static Asset LoadAsset(string path)
	{
		Asset asset = new Asset(Resources.Load(path), path);
		return asset;
	}

	public static List<Asset> LoadAssetFolder(string path)
	{
		status = path == "DEFAULT_GAMEASSET_FOLDER" ? "Loading General Assets." : string.Format("Loading {0}.", path.ToUpper());
		List<Asset> assetFolder = new List<Asset>();
		foreach (UnityEngine.Object obj in Resources.LoadAll(path))
		{
			Asset asset = new Asset(obj, path == "DEFAULT_GAMEASSET_FOLDER" ? obj.name : path + "/" + obj.name);
			assetFolder.Add(asset);
		}
		return assetFolder;
	}

	public static string status;

	public struct Asset
	{
		public UnityEngine.Object assetObject;

		public string assetPath;

		public Asset(UnityEngine.Object obj, string path)
		{
			this.assetObject = obj;
			this.assetPath = path;
		}
	}

	public static List<string> folderList = new List<string>
	{
		"DEFAULT_GAMEASSET_FOLDER",
		"guns",
		"guys",
		"music",
		"zombies"
	};

	public static void CheckInit()
	{
		if (!initialized)
		{
			InitializeAssets();
		}
	}

	public static void InitializeAssets()
	{
		foreach (string path in folderList)
		{
			CachedAssets.AddRange(LoadAssetFolder(path));
		}
		foreach (Asset asset in CachedAssets)
		{
			Debug.Log(asset.assetPath);
		}
		status = "Finished.";
		initialized = true;
	}

	public static bool initialized;

	public static List<Asset> CachedAssets = new List<Asset>();
}
