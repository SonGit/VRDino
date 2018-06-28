using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Spear : Weapon {

	public Collider spearTipCollider;
	public bool enoughForce;

	private bool spinning;
	private bool returning;
	[SerializeField]
	private float spinSpeed = 700;

	private float goUpTime = 0.25f;
	[SerializeField]
	private float flyTime = 1;

	protected override void Awake()
	{
		base.Awake ();
		InteractableObjectGrabbed += new InteractableObjectEventHandler(ResetPosition);
	}

	void Start()
	{
		Initialize ();
	}

	float angle;

	protected override void Update()
	{
		base.Update ();

		CalculateVelocity ();

		if (spinning) {
			transform.eulerAngles += new Vector3 (0, spinSpeed * Time.deltaTime, 0);
		}

		if (IsGrabbed ()) {
			if (!twoHanded)
				transform.localEulerAngles = Vector3.zero;
			
			transform.localPosition = Vector3.zero;
			interactableRigidbody.isKinematic = true;
		} else {
			if(!returning)
			interactableRigidbody.isKinematic = false;
		}


//		print ("velocity  " +velocity);
	}

	public override void Thrown(bool enoughForce = true)
	{
		base.Thrown ();
		this.enoughForce = enoughForce;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate ();
	}
		
	protected override void OnCollisionEnter (Collision collision)
	{
		base.OnCollisionEnter (collision);

		CheckIfThrowRock (collision.gameObject);

		if (spinning) {
			CheckIfEnemyAndDealDamage (collision,collision.contacts[0].point,85);
		}
	}

	ThrowObject rock;
	private void CheckIfThrowRock(GameObject rockGO)
	{
		rock = rockGO.GetComponent<ThrowObject> ();
		// If player indeed hit the enemy
		if (rock != null) {
			rock.Bounce ();
			AudioManager.instance.PlayClip (AudioManager.SoundFX.Impact,transform.position);
			//VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, .5f, 0.5f, 0.01f);
		}
	}


	public void ReturnToHand()
	{
		float distanceToPlayer = Vector3.Distance (transform.position,Player.instance.transform.position);
		if (distanceToPlayer > 5) {
			StartCoroutine (ReturnToHand_Sequence ());
		} else {
			Player.instance.GrabWeapon (gameObject);
		}
	}

	IEnumerator ReturnToHand_Sequence()
	{
		returning = true;
		transform.eulerAngles = Vector3.zero;
		interactableRigidbody.isKinematic = true;

		spinning = true;
		iTween.MoveBy(gameObject, iTween.Hash("y", 2, "time", goUpTime, "easeType", iTween.EaseType.linear));
		yield return new WaitForSeconds (goUpTime);
		iTween.MoveTo(gameObject, iTween.Hash("position", Player.instance.transform.position, "time", flyTime, "easeType", iTween.EaseType.linear));
		yield return new WaitForSeconds (flyTime);
		spinning = false;

		interactableRigidbody.isKinematic = false;
		returning = false;

		Player.instance.GrabWeapon (gameObject);
	}


}
