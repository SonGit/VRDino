using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RootMotion.Dynamics;

public class Dino : Enemy {

	public float countThrow;
	public bool isThrowing;

	// Use this for initialization
	IEnumerator Start () {

		gameObject.SetActive (false);
		Destroy ();
		yield return new WaitForSeconds (1);
	}
		
	// Update is called once per frame
	void Update () {
		Loop ();

		if(Input.GetKeyDown(KeyCode.P))
			{
			Stun ();
			}

		if (Input.GetKeyDown (KeyCode.I) && _living) 
		{
			OnHit (1000);
		}

		ThrowTrigger ();
	}
		

	protected override void ApplyPhysics()
	{
		Rigidbody[] rbs = this.GetComponentsInChildren<Rigidbody> ();
		foreach(Rigidbody rb in rbs)
		{
			rb.useGravity = true;
			rb.isKinematic = false;
			rb.constraints = RigidbodyConstraints.None;
		
			float rand = Random.Range (5f,7f);
			Vector3 v = rb.velocity;

			if (rb.velocity.x > 9f ) {
				v.x = rand;
				rb.velocity = v;
			}

			if (rb.velocity.y > 9f ) {
				v.y = rand;
				rb.velocity = v;
			}

			if (rb.velocity.z > 9f ) {
				v.z = rand;
				rb.velocity = v;
			}
		}

		bodyIK.enabled = false;
		animator.enabled = false;
	}

	public override void RandomHitSound ()
	{
		int ranHitSound = Random.Range (0,2);
		if (ranHitSound == 0) {
			AudioManager.instance.PlayClip (AudioManager.SoundFX.FatDinoHit1,transform.position);
		} else {
			AudioManager.instance.PlayClip (AudioManager.SoundFX.FatDinoHit2,transform.position);
		}
	}

	public override void RandomDieSound ()
	{
		int ranDieSound = Random.Range (0,2);
		if (ranDieSound == 0) {
			AudioManager.instance.PlayClip (AudioManager.SoundFX.FatDinoDie1,transform.position);
		} else {
			AudioManager.instance.PlayClip (AudioManager.SoundFX.FatDinoDie2,transform.position);
		}
	}

	public override void OnLive ()
	{
		gameObject.SetActive (true);
		Initialize ();
		isThrowing = false;
	}


	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}

	void ThrowTrigger ()
	{
		if (stateController.playerReference == null) {
			return;
		}

		float distanceToPlayer = Vector3.Distance (stateController.playerReference.transform.position,transform.position);
	
		if (distanceToPlayer < 30) {
			return;
		}

		if (!isThrowing) {
			countThrow += Time.deltaTime;
			if (countThrow > 3) {
				Throw ();
				countThrow = 0;
			}
		}

	}

	void Throw()
	{
		isThrowing = true;

		stateController.AIEnabled = false;
		agent.enabled = false;
		animator.SetInteger ("State",0);
		animator.SetTrigger ("Attack2Trigger");
	}

	#region animation events
	void StartThrow()
	{
		ThrowObject obj = ObjectPool.instance.GetRockThrow ();

		if (stateController.playerReference != null) {
			obj.Live ();
			obj.transform.position = transform.position;
			obj.Launch (stateController.playerReference.transform.position, transform);
		} else {
			isThrowing = false;
		}
	}

	void EndThrow()
	{
		stateController.AIEnabled = true;
		agent.enabled = true;
		isThrowing = false;
	}

	#endregion

}
