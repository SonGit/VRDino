﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Weapon : VRTK_InteractableObject {

	// Reference to the controller holding this weapon
	protected VRTK_ControllerReference controllerReference;
	// is the weapon being thrown?
	protected bool inFlight;
	// initial angle before the weapon is thrown
	protected Vector3 initialAngle;
	// local collider reference 
	public BoxCollider weaponCollider;
	// melee collider size - for melee uses only
	public Vector3 meleeScale;
	// thrown collider size - for when weapon is thrown 
	public Vector3 thrownScale;
	//Mainly use for calculating weapon velocity
	public Transform weaponTip;
	// Min force player must physically applied to have a valid hit
	public float minForce;

	// for weapon velocity calculating
	private Vector3 previous;
	private float velocity;

	// Cache
	protected Enemy enemy;

	// Use this for initialization
	protected void Initialize () {
		if(weaponCollider != null)
		weaponCollider.isTrigger = false;
	}

	protected void Loop () {
		// Physics hack
		if (inFlight) {
			transform.eulerAngles = new Vector3( transform.eulerAngles.x + .25f, initialAngle.y , initialAngle.z );
			//transform.eulerAngles = new Vector3 (transform.eulerAngles.x, initialAngle.y, initialAngle.z);
		} 
	}

	public AudioSource whoosh;
	// Put this in FixedUpdate()
	protected void CalculateVelocity()
	{
		velocity = ((weaponTip.position - previous).magnitude) / Time.deltaTime;

		previous = weaponTip.position;

		if (whoosh != null) {
			if (VRTK_ControllerReference.IsValid(controllerReference) && IsGrabbed() && velocity > 8)
			{
				if (!whoosh.isPlaying)
					whoosh.Play ();
			}
		}
	}

	// when weapon is thrown
	public virtual void Thrown(bool enoughForce = true)
	{
		inFlight = true;
		initialAngle = transform.eulerAngles;

		weaponCollider.isTrigger = false;
		weaponCollider.size = thrownScale;
	}

	// reset position on grab
	public override void Grabbed(VRTK_InteractGrab grabbingObject)
	{
		base.Grabbed(grabbingObject);
		controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);

		if(weaponCollider != null)
		weaponCollider.size = meleeScale;
		//interactableRigidbody.isKinematic = false;
		print("Grabbed");
	}

	protected void ResetPosition(object sender, InteractableObjectEventArgs e)
	{
		print ("ResetPosition");
		StartCoroutine (ResetPosition_async());
	}

	IEnumerator ResetPosition_async()
	{
		yield return new WaitForEndOfFrame();
		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;
	}

	// On hit something when thrown
	protected virtual void OnHitSurface(Transform hitSurface)
	{
		print ("OnHitSurface" + hitSurface.name);		
		inFlight = false;
	}
	private float impactMagnifier = 120f;
	private float collisionForce = 0f;
	private float maxCollisionForce = 4000f;
	protected virtual void OnCollisionEnter(Collision collision)
	{
		// This means that the weapon is thrown and hit a surface
		if (inFlight) {
			OnHitSurface (collision.transform);
			CheckIfEnemyAndDealDamage (collision,collision.contacts[0].point,velocity);
			inFlight = false;
		}

		if (VRTK_ControllerReference.IsValid(controllerReference) && IsGrabbed() && velocity > 8)
		{
			collisionForce = VRTK_DeviceFinder.GetControllerVelocity(controllerReference).magnitude * impactMagnifier;
			var hapticStrength = collisionForce / maxCollisionForce;
			VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, hapticStrength, 0.5f, 0.01f);
			CheckIfEnemyAndDealDamage (collision,collision.contacts[0].point,velocity);

			AudioManager.instance.PlayClip (AudioManager.SoundFX.Impact,transform.position);
		}
	}
		
	protected void CheckIfEnemyAndDealDamage(Collision collision,Vector3 collisionPoint,float force)
	{
		enemy = collision.transform.root.GetComponent<Enemy> ();
		// If player indeed hit the enemy
		if (enemy != null) {
			enemy.Hit (collision.collider,collisionPoint,force);
		}
	}

	public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
	{
		base.Ungrabbed (previousGrabbingObject);
		weaponCollider.isTrigger = false;
		//weaponCollider.size = thrownScale;
		print("Ungrabbed");
	}


}
