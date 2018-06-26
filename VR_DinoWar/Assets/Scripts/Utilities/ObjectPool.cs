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

	GenericObject<Dino_LongLeg> dino_LongLeg;

	GenericObject<StunEffect> stunEffect;

	GenericObject<Audio> audioS;

	GenericObject<SmokeEffect> smokeEffect;

	GenericObject<ExplosionBoom> explosionBoom;

	GenericObject<ThrowObject> rockThrow;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		hitNumbers = new GenericObject<HitNumber>(ObjectFactory.PrefabType.HitNumber,1);
		textHitRandoms = new GenericObject<TextHitRandom>(ObjectFactory.PrefabType.TextHitRandom,1);
		deathSkulls = new GenericObject<DeathSkull>(ObjectFactory.PrefabType.DeathSkull,1);
		dinos = new GenericObject<Dino>(ObjectFactory.PrefabType.Dino,1);
		dino_LongLeg = new GenericObject<Dino_LongLeg>(ObjectFactory.PrefabType.Dino_LongLeg,1);
		stunEffect = new GenericObject<StunEffect>(ObjectFactory.PrefabType.StunEffect,1);
		audioS = new GenericObject<Audio>(ObjectFactory.PrefabType.AudioSource,1);
		smokeEffect = new GenericObject<SmokeEffect> (ObjectFactory.PrefabType.SmokeEffect, 1);
		explosionBoom = new GenericObject<ExplosionBoom> (ObjectFactory.PrefabType.Explosion, 1);
		rockThrow = new GenericObject<ThrowObject> (ObjectFactory.PrefabType.Rock, 15);
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

	public Enemy GetEnemy(Enemy enemy)
	{
		if (enemy is Dino) {
			//dinos = enemy as Dino;
			return dinos.GetObj ();
		}

		if (enemy is Dino_LongLeg) {
			//dino_LongLeg = enemy as Dino_LongLeg;
			return dino_LongLeg.GetObj ();
		}

		return null;

	}

	public StunEffect GetStunEffect()
	{
		return stunEffect.GetObj ();
	}

	public Audio GetAudioSoure()
	{
		return audioS.GetObj ();
	}

	public SmokeEffect GetSmokeEffect()
	{
		return smokeEffect.GetObj ();
	}

	public ExplosionBoom GetExplosionBoom()
	{
		return explosionBoom.GetObj ();
	}

	public ThrowObject GetRockThrow()
	{
		return rockThrow.GetObj ();
	}
}
