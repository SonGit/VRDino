﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName ="PluggableAI/Actions/CheckAttackDecision")]
public class CheckAttackDecision : Decision {

	public override bool Decide (StateController controller)
	{
		bool decide = CheckMaxAttkEnemy (controller);
		return decide;
	}

	bool CheckMaxAttkEnemy(StateController controller)
	{
		if (controller.playerReference == null)
			return false;

		if (controller.playerReference.GetCurrentEngagedEnemy() < Utility.MAX_ATTK_ENEMY) {

			if(Player.instance.GetCurrentEngagedEnemy() < Utility.MAX_ATTK_ENEMY)
				Player.instance.IncreaseEngagedEnemy();
			
			return true;

		} else {

			return false;
		}

	}
}
