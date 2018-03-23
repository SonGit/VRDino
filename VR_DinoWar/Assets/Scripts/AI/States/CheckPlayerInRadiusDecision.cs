﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName ="PluggableAI/Actions/CheckPlayerInRadiusDecision")]
public class CheckPlayerInRadiusDecision : Decision {

	public override bool Decide (StateController controller)
	{
		bool decide = CheckAttack (controller);
		return decide;
	}

	bool CheckAttack(StateController controller)
	{
		float distanceToPlayer = Vector3.Distance (controller.enemy.transform.position,controller.playerReference.transform.position);

		if (distanceToPlayer < controller.minimumRange) {
			return true;
		} else {
			return false;
		}

	}
}
