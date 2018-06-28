using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Spear : Weapon {

	public Collider spearTipCollider;
	public bool enoughForce;

	private bool spinning;
	[SerializeField]
	private float spinSpeed = 700;

	private float goUpTime = 0.25f;
	private float flyTime = .5f;

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
			if(!twoHanded)
			transform.localEulerAngles = Vector3.zero;
			
			transform.localPosition = Vector3.zero;
			interactableRigidbody.isKinematic = true;
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
			CheckIfEnemyAndDealDamage (collision,collision.contacts[0].point,45);
		}
	}

	ThrowObject rock;
	private void CheckIfThrowRock(GameObject rockGO)
	{
		rock = rockGO.GetComponent<ThrowObject> ();
		// If player indeed hit the enemy
		if (rock != null) {
			rock.Bounce ();
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
		transform.eulerAngles = Vector3.zero;
		interactableRigidbody.isKinematic = true;

		spinning = true;
		iTween.MoveBy(gameObject, iTween.Hash("y", 2, "time", goUpTime, "easeType", iTween.EaseType.linear));
		yield return new WaitForSeconds (goUpTime);
		iTween.MoveTo(gameObject, iTween.Hash("position", Player.instance.transform.position, "time", flyTime, "easeType", iTween.EaseType.linear));
		yield return new WaitForSeconds (flyTime);
		spinning = false;

		interactableRigidbody.isKinematic = false;

		Player.instance.GrabWeapon (gameObject);
	}


}
