using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : Cacheable {
	
	public AudioSource audioSource ;

	public override void OnLive ()
	{
		audioSource.Play();
	}

	public override void OnDestroy ()
	{
		
	}
		
		
	// Use this for initialization
	void Start () {
		Destroy ();
	}

	float timeCount = 0;
	
	// Update is called once per frame
	void Update () {

		if (!_living)
			return;

		timeCount += Time.deltaTime;

		if (timeCount > audioSource.clip.length) {
			timeCount = 0;
			Destroy ();
		}

	}
}
