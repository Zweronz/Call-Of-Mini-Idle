using System.Collections.Generic;

public static class Storager
{
	private const bool useCryptoPlayerPrefs = true;

	private static bool iCloudAvailable;

	private static IDictionary<string, int> _keychainCache;

	private static int _readCount;

	private static int _writeCount;

	public static int ReadCount
	{
		get
		{
			return _readCount;
		}
	}

	public static int WriteCount
	{
		get
		{
			return _writeCount;
		}
	}

	static Storager()
	{
		iCloudAvailable = false;
		_keychainCache = new Dictionary<string, int>();
		int salt = 0x6B3C41E4 ^ 2083243184;
		CryptoPlayerPrefs.setSalt(salt);
		CryptoPlayerPrefs.useRijndael(true);
		CryptoPlayerPrefs.useXor(true);
	}

	public static void Initialize(bool cloudAvailable)
	{
	}

	public static bool Synchronize()
	{
		CryptoPlayerPrefs.Save();
		return true;
	}

	public static bool HasKey(string key)
	{
		return CryptoPlayerPrefs.HasKey(key);
	}

	public static void RemoveObjectForKey(string key)
	{
		CryptoPlayerPrefs.DeleteKey(key);
	}

	public static void RemoveAll()
	{
		CryptoPlayerPrefs.DeleteAll();
	}

	public static void SetInt(string key, int val)
	{
		_writeCount++;
		CryptoPlayerPrefs.SetInt(key, val);
	}

	public static int GetInt(string key)
	{
		_readCount++;
		if (CryptoPlayerPrefs.HasKey(key))
		{
			return CryptoPlayerPrefs.GetInt(key);
		}
		return 0;
	}
	public static void SetString(string key, string val)
	{
		_writeCount++;
		CryptoPlayerPrefs.SetString(key, val);
	}

	public static string GetString(string key)
	{
		_readCount++;
		if (CryptoPlayerPrefs.HasKey(key))
		{
			return CryptoPlayerPrefs.GetString(key);
		}
		return "";
	}
	public static void SetFloat(string key, float val)
	{
		_writeCount++;
		CryptoPlayerPrefs.SetFloat(key, val);
	}

	public static float GetFloat(string key)
	{
		_readCount++;
		if (CryptoPlayerPrefs.HasKey(key))
		{
			return CryptoPlayerPrefs.GetFloat(key);
		}
		return 0f;
	}
}
