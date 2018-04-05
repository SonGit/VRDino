using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
	public static AudioManager instance;

	private Dictionary<SoundFX,AudioClip> clips;

	void Awake()
	{
		instance = this;
	}

	IEnumerator Start()
	{
		clips = new Dictionary<SoundFX, AudioClip> {

			{ SoundFX.None, null },
			{ SoundFX.AmbienceBird, Resources.Load<AudioClip>("Sounds/Effects/AmbienceBird") },
			{ SoundFX.AmbienceRiver, Resources.Load<AudioClip>("Sounds/Effects/AmbienceRiver") },
			{ SoundFX.AmbienceWind, Resources.Load<AudioClip>("Sounds/Effects/AmbienceWind") },
			{ SoundFX.ArrowFly, Resources.Load<AudioClip>("Sounds/Effects/ArrowFly") },
			{ SoundFX.FatDinoAttack1, Resources.Load<AudioClip>("Sounds/Effects/FatDinoAttack1") },
			{ SoundFX.FatDinoAttack2, Resources.Load<AudioClip>("Sounds/Effects/FatDinoAttack2") },
			{ SoundFX.FatDinoDie1, Resources.Load<AudioClip>("Sounds/Effects/FatDinoDie1") },
			{ SoundFX.FatDinoDie2, Resources.Load<AudioClip>("Sounds/Effects/FatDinoDie2") },
			{ SoundFX.FatDinoHit1, Resources.Load<AudioClip>("Sounds/Effects/FatDinoHit1") },
			{ SoundFX.FatDinoHit2, Resources.Load<AudioClip>("Sounds/Effects/FatDinoHit2") },
			{ SoundFX.FatDinoPee, Resources.Load<AudioClip>("Sounds/Effects/FatDinoPee") },
			{ SoundFX.Impact, Resources.Load<AudioClip>("Sounds/Effects/Impact") },
			{ SoundFX.Whoosh, Resources.Load<AudioClip>("Sounds/Effects/Whoosh") },
			{ SoundFX.LongbowPullBack01, Resources.Load<AudioClip>("Sounds/Effects/LongbowPullBack01") },
			//{ SoundFX.Whoosh, Resources.Load<AudioClip>("Sounds/Effects/Whoosh") },
		};

		yield return new WaitForSeconds (1);
	}

	public enum SoundFX
	{
		None,
		AmbienceBird,
		AmbienceRiver,
		AmbienceWind,
		ArrowFly,
		FatDinoAttack1,
		FatDinoAttack2,
		FatDinoConfused,
		FatDinoDie1,
		FatDinoDie2,
		FatDinoHit1,
		FatDinoHit2,
		FatDinoPee,
		Impact,
		Whoosh,
		LongbowPullBack01,
	}

	public void PlayClip(SoundFX soundFX,Vector3 worldPos,float volume = 1)
	{
		AudioClip clip;

		if (clips.TryGetValue (soundFX, out clip)) {
			Audio audio = ObjectPool.instance.GetAudioSoure ();
			audio.audioSource.clip = clip;
			audio.audioSource.volume = volume;
			audio.transform.position = worldPos;
			audio.Live ();
		}
	}


}
