using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBoom : Cacheable {

	ParticleSystem[] particles;

	void Start () {
		particles = this.GetComponentsInChildren<ParticleSystem> ();
		Destroy ();

	}

	public override void OnLive ()
	{
		particles = this.GetComponentsInChildren<ParticleSystem> ();
		foreach (ParticleSystem particle in particles) {
			particle.Play ();
		}
	}

	public override void OnDestroy ()
	{

	}

	float timeCount = 0;

	void Update()
	{
		if (!_living)
			return;

		timeCount += Time.deltaTime;

		if (timeCount > 1) {
			timeCount = 0;
			Destroy ();
		}

	}

}
