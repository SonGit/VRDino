using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffect : Cacheable {

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
}
