using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName ="PluggableAI/Actions/IdleAction")]
public class IdleAction : Action {
	
	public override void Init (StateController controller)
	{
		int rand = Random.Range (4,8);
		controller.enemy.animator.SetInteger ("State", rand);
		AudioManager.instance.PlayClip (AudioManager.SoundFX.FatDinoPee,controller.enemy.transform.position,0.5f);
		controller.enemy.isIdleDone = false;
	//	controller.enemy.LocalAvoidanceOn ();
		controller.enemy.FaceTarget(controller.playerReference.transform.position);
		controller.enemy.onIdleAnimDone += DoneIdle;
	}

	public override void Act (StateController controller)
	{
		
	}

	private void Idling(StateController controller)
	{
		
	}

	private void DoneIdle(StateController controller)
	{
		controller.enemy.onIdleAnimDone -= DoneIdle;
		controller.enemy.LocalAvoidanceOff ();
	}
		

}
