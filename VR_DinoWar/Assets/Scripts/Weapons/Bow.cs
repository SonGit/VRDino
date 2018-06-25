using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {

	bool enoughForce;

	protected override void Awake()
	{
		base.Awake ();
	}

	void Start()
	{
		Initialize ();
	}

	protected override void Update()
	{
		base.Update ();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate ();
	}

	public override void Thrown(bool enoughForce = true)
	{
		base.Thrown ();
		this.enoughForce = enoughForce;
	}

	protected override void OnHitSurface(Transform hitSurface)
	{
		base.OnHitSurface (hitSurface);
		DebugConsole.instance.ShowColliderHeadQuiver (false);
	}

}
