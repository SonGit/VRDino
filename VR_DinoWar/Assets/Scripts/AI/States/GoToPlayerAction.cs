﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="PluggableAI/Actions/GoToPlayerAction")]
public class GoToPlayerAction : Action {
	
	public override void Init (StateController controller)
	{

		controller.enemy.LocalAvoidanceOff ();
	}

	public override void Act (StateController controller)
	{
		GoToPlayer (controller);
	}

	private void GoToPlayer(StateController controller)
	{
		controller.enemy.animator.SetInteger ("State", 1);

		float distanceToPlayer = Vector3.Distance(controller.transform.position,controller.playerReference.transform.position);

		if (distanceToPlayer <= controller.minimumRange) {

		} else {
			if (!controller.enemy.agent.enabled) {
				controller.enemy.agent.enabled = true;
			}
			controller.enemy.agent.destination = controller.playerReference.transform.position;
		}
	}

}
