using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;


public class EnemyGrab : VRTK_BaseJointGrabAttach {


	Enemy enemy;
	public Rigidbody rbGrab;
	public bool isThrow;
	public bool isHold;


	// Use this for initialization
	void Start () {
		isHold = false;
		isThrow = false;
		enemy = this.GetComponent<Enemy> ();
	}

	[Tooltip("Maximum force the joint can withstand before breaking. Infinity means unbreakable.")]
	public float breakForce = 1500f;

	protected override void CreateJoint(GameObject obj)
	{
		givenJoint = obj.AddComponent<FixedJoint>();
		givenJoint.breakForce = (grabbedObjectScript.IsDroppable() ? breakForce : Mathf.Infinity);
		base.CreateJoint(obj);
	}

	protected override void ThrowReleasedObject(Rigidbody objectRigidbody)
	{
		if (grabbedObjectScript != null)
		{
			VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(grabbedObjectScript.GetGrabbingObject());
			if (VRTK_ControllerReference.IsValid(controllerReference) && controllerReference.scriptAlias != null)
			{
				VRTK_InteractGrab grabbingObjectScript = controllerReference.scriptAlias.GetComponent<VRTK_InteractGrab>();
				if (grabbingObjectScript != null)
				{
					Transform origin = VRTK_DeviceFinder.GetControllerOrigin(controllerReference);

					Vector3 velocity = VRTK_DeviceFinder.GetControllerVelocity(controllerReference);
					Vector3 angularVelocity = VRTK_DeviceFinder.GetControllerAngularVelocity(controllerReference);
					float grabbingObjectThrowMultiplier = grabbingObjectScript.throwMultiplier;

					if (origin != null)
					{
						objectRigidbody.velocity = origin.TransformVector(velocity) * (grabbingObjectThrowMultiplier * throwMultiplier);
						objectRigidbody.angularVelocity = origin.TransformDirection(angularVelocity);

						if (enemy != null && enemy.hitPoints > 0) {
							enemy.OnOffCollider (true,false);
							isThrow = true;
							rbGrab = this.GetComponent<Rigidbody> ();
							rbGrab.useGravity = true;
						}
						else {

						}
					}
					else
					{
						objectRigidbody.velocity = velocity * (grabbingObjectThrowMultiplier * throwMultiplier);
						objectRigidbody.angularVelocity = angularVelocity;
					}

					if (throwVelocityWithAttachDistance)
					{
						Collider rigidbodyCollider = objectRigidbody.GetComponentInChildren<Collider>();
						if (rigidbodyCollider != null)
						{
							Vector3 collisionCenter = rigidbodyCollider.bounds.center;
							objectRigidbody.velocity = objectRigidbody.GetPointVelocity(collisionCenter + (collisionCenter - transform.position));
						}
						else
						{
							objectRigidbody.velocity = objectRigidbody.GetPointVelocity(objectRigidbody.position + (objectRigidbody.position - transform.position));
						}
					}
				}
			}
		}

	}


	protected override void SetSnappedObjectPosition (GameObject obj)
	{
		
	}
		

	IEnumerator OnTriggerEnter (Collider collider)
	{
		if (isThrow) 
		{
			Enemy otherEnemy = collider.GetComponentInParent<Enemy> ();

			if (name == "DinoFat(Clone)" && collider.name == "Spine1_M") {
				if (enemy != null && otherEnemy != null ) {
					enemy.OnHit (250);
					otherEnemy.OnHit (250);
				}
			} 

			else if (name == "DinoFat(Clone)" && collider.name == "DinolonglegHub001"){
				if (enemy != null && otherEnemy != null) {
					enemy.OnHit (250);
					otherEnemy.OnHit (250);
				}
			}
				
			else if (name == "Dino_LongLeg(Clone)" && collider.name == "DinolonglegHub001") {
				if (enemy != null && otherEnemy != null) {
					enemy.OnHit (250);
					otherEnemy.OnHit (250);
				}
			}

			else if (name == "Dino_LongLeg(Clone)" && collider.name == "Spine1_M"){
				if (enemy != null && otherEnemy != null) {
					enemy.OnHit (250);
					otherEnemy.OnHit (250);
				}
			}

			else if (collider.gameObject.tag == "Terrain 1"){
				if (enemy != null) {
					enemy.OnHit (250);
				}
			}

			yield return new WaitForSeconds(2);
			isThrow = false;

		}
	}

	public void ApplyPhysicsGrab ()
	{
		if (isHold) {
			isHold = false;
			rbGrab = this.GetComponent<Rigidbody> ();
			rbGrab.isKinematic = true;
			//Inventory.instance.leftHand.GetComponent<VRTK_InteractTouch>().isActiveCollider = false;
			//Inventory.instance.rightHand.GetComponent<VRTK_InteractTouch> ().isActiveCollider = false;
		}

	}



}
