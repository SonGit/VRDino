using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDino : Enemy {

	// Use this for initialization
	void Start () {
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		Loop ();
	}

	public override void RandomHitSound ()
	{
		
	}

	public override void RandomDieSound ()
	{
		
	}

	public override void OnLive ()
	{

	}

	public override void OnDestroy ()
	{

	}
}
