using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	public void ChangeClip(string AC)
	{
		if (Global.mute || Global.muteMusic)
		{
			return;
		}
		StartCoroutine(FadeOut(GameAsset.Load<AudioClip>("music/" + AC)));
	}

	public List<AudioSource> tempSources = new List<AudioSource>();

	public void PlayClip(AudioClip AC, Vector2 pitchRange = default(Vector2), AudioPlayType APT = AudioPlayType.normal, float volume = 1f)
	{
		if (Global.mute)
		{
			return;
		}
		volume *= (Global.currentVolume / 100f);
		bool pitched = pitchRange != default(Vector2);
		AudioSource newAS = new GameObject("AudioCon Temp Source").AddComponent<AudioSource>();
		newAS.transform.parent = this.transform;
		newAS.pitch = (pitched) ? UnityEngine.Random.Range(pitchRange.x, pitchRange.y) : 1f;
		if (APT == AudioPlayType.normal)
		{
			newAS.clip = AC;
			newAS.volume = volume;
			newAS.Play();
			newAS.gameObject.AddComponent<AudioControllerChild>();
			tempSources.Add(newAS);
			return;
		}
		if (APT == AudioPlayType.fadeIn)
		{
			newAS.clip = AC;
			newAS.volume = 0f;
			newAS.Play();
			newAS.gameObject.AddComponent<AudioControllerChild>();
			tempSources.Add(newAS);
			while (newAS.volume < Global.currentVolume / 100f)
			{
				newAS.volume += Time.deltaTime;
			}
			return;
		}
		if (APT == AudioPlayType.fadeOut)
		{
			newAS.clip = AC;
			newAS.volume = Global.currentVolume / 100f;
			newAS.Play();
			newAS.gameObject.AddComponent<AudioControllerChild>();
			tempSources.Add(newAS);
			while (newAS.volume > 0f)
			{
				newAS.volume -= Time.deltaTime;
			}
			return;
		}
	}

	private bool fading;

	void Update()
	{
		if (fading)
		{
			return;
		}
		GetComponent<AudioSource>().volume = Global.currentVolume / 100f;
	}

	public IEnumerator FadeOut(AudioClip AC)
	{
		if (Global.mute)
		{
			yield break;
		}
		fading = true;
		if (GetComponent<AudioSource>().clip != null)
		{
		while (GetComponent<AudioSource>().volume > 0f)
		{
			yield return new WaitForSeconds(0.01f);
			GetComponent<AudioSource>().volume -= 0.03f;
		}
		}
		GetComponent<AudioSource>().clip = AC;
		GetComponent<AudioSource>().Play();
		while (GetComponent<AudioSource>().volume < Global.currentVolume / 100f)
		{
			yield return new WaitForSeconds(0.01f);
			GetComponent<AudioSource>().volume += 0.03f;
		}
		fading = false;
	}
}
