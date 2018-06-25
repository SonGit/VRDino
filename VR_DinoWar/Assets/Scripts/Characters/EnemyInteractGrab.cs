using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;

public class EnemyInteractGrab : VRTK_InteractableObject {

	Enemy enemy;
	EnemyGrab enemyGrab;
	// Use this for initialization
	void Start () {
		enemyGrab = this.GetComponent<EnemyGrab> ();
		enemy = this.GetComponent<Enemy> ();
	}

	protected override void PrimaryControllerGrab(GameObject currentGrabbingObject)
	{
		if (snappedInSnapDropZone)
		{
			ToggleSnapDropZone(storedSnapDropZone, false);
		}
		ForceReleaseGrab();
		RemoveTrackPoint();
		grabbingObjects.Add(currentGrabbingObject);
		SetTrackPoint(currentGrabbingObject);
		if (!IsSwappable() && enemy != null)
		{
			previousIsGrabbable = isGrabbable;
			isGrabbable = false;
			enemy.stateController.enabled = false;
			enemy.agent.enabled = false;
			enemyGrab.isHold = true;
			//Inventory.instance.leftHand.GetComponent<VRTK_InteractTouch> ().isActiveCollider = true;
			//Inventory.instance.rightHand.GetComponent<VRTK_InteractTouch> ().isActiveCollider = true;

		}
	}
		
}
