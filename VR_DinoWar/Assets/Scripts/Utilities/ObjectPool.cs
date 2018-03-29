using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectPool gameObject

public class ObjectPool : MonoBehaviour {

	public static ObjectPool instance;

	GenericObject<HitNumber> hitNumbers;

	GenericObject<TextHitRandom> textHitRandoms;

	GenericObject<DeathSkull> deathSkulls;

	GenericObject<Dino> dinos;

	GenericObject<StunEffect> stunEffect;


	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		hitNumbers = new GenericObject<HitNumber>(ObjectFactory.PrefabType.HitNumber,10);
		textHitRandoms = new GenericObject<TextHitRandom>(ObjectFactory.PrefabType.TextHitRandom,10);
		deathSkulls = new GenericObject<DeathSkull>(ObjectFactory.PrefabType.DeathSkull,10);
		dinos = new GenericObject<Dino>(ObjectFactory.PrefabType.Dino,10);
		stunEffect = new GenericObject<StunEffect>(ObjectFactory.PrefabType.StunEffect,10);
	}

	public HitNumber GetHitNumber()
	{
		return hitNumbers.GetObj ();
	}

	public TextHitRandom GetTextHitRandom()
	{
		return textHitRandoms.GetObj ();
	}	

	public DeathSkull GetDeathSkulls()
	{
		return deathSkulls.GetObj ();
	}

	public Dino GetDinos()
	{
		return dinos.GetObj ();
	}

	public StunEffect GetStunEffect()
	{
		return stunEffect.GetObj ();
	}
}
