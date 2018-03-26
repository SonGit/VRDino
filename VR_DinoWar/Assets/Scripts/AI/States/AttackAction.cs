using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="PluggableAI/Actions/AttackAction")]
public class AttackAction : Action {

	public override void Init (StateController controller)
	{
		controller.enemy.animator.SetInteger ("State", 0);
		controller.enemy.onEndAttackAnim += EndAttackAnim;

	}

	public override void Act (StateController controller)
	{
		Attack (controller);
	}

	private void Attack(StateController controller)
	{
		controller.enemy.countAttack += Time.deltaTime;
		if (controller.enemy.countAttack > controller.enemy.rate && !controller.enemy.isAttack) 
		{
			RandomAttackAnimation (controller);
			controller.enemy.isAttack = true;
		}
			
		float distanceToPlayer = Vector3.Distance(controller.transform.position,controller.playerReference.transform.position);

		if (distanceToPlayer <= 4) {
			controller.enemy.FaceTarget (controller.playerReference.transform.position);
			controller.enemy.LocalAvoidanceOn ();
		} else {
			controller.enemy.LocalAvoidanceOff ();
		}

	}

	private void EndAttackAnim(StateController controller)
	{
		Debug.Log ("HIT ME");
	}

	private void RandomAttackAnimation (StateController controller)
	{
		int ranAttack = Random.Range (0,2);
		if (ranAttack == 0) {
			controller.enemy.animator.SetTrigger ("Attack1Trigger");
		} else {
			controller.enemy.animator.SetTrigger ("Attack2Trigger");
		}
	}
		
}
