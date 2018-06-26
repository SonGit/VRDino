using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectFactory gameObject
public class ObjectFactory: MonoBehaviour {

	public static ObjectFactory instance;

	void Awake()
	{
		instance = this;
	}

	public enum PrefabType
	{
		None,
		HitNumber,
		TextHitRandom,
		DeathSkull,
		StunEffect,
		Dino,
		Dino_LongLeg,
		AudioSource,
		SmokeEffect,
		Explosion,
		Rock,
	}

	public Dictionary<PrefabType,string> PrefabPaths = new Dictionary<PrefabType, string> {
		
		{ PrefabType.None, "" },
		{ PrefabType.HitNumber, "Prefabs/HitNumber" },
		{ PrefabType.TextHitRandom, "Prefabs/TextHitRandom" },
		{ PrefabType.DeathSkull, "Prefabs/DeathSkull" },
		{ PrefabType.Dino, "Prefabs/DinoFat" },
		{ PrefabType.Dino_LongLeg, "Prefabs/Dino_LongLeg" },
		{ PrefabType.StunEffect, "Prefabs/StunEffect" },
		{ PrefabType.AudioSource, "Prefabs/AudioSource" },
		{ PrefabType.SmokeEffect, "Prefabs/SmokeEffect" },
		{ PrefabType.Explosion, "Prefabs/Explosion" },
		{ PrefabType.Rock, "Prefabs/Rock" },
	};

	public GameObject MakeObject(PrefabType type)
	{
		string path;
		if (PrefabPaths.TryGetValue (type, out path)) {
			return (Instantiate (Resources.Load (path, typeof(GameObject))) as GameObject);
		}
		print ("NULL");
		return null;
	}

}
