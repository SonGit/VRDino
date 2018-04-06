using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RootMotion.Dynamics;

public class Dino : Enemy {

	// Use this for initialization
	IEnumerator Start () {
		Initialize ();

		Rigidbody[] rbs = this.GetComponentsInChildren<Rigidbody> ();
		foreach(Rigidbody rb in rbs)
		{
			rb.useGravity = false;
			rb.isKinematic = false;
		}
			
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

		if (Input.GetKeyDown (KeyCode.D) && _living) 
		{
			OnHit (1000);
		}

	}

	private void OnCollisionEnter(Collision collision)
	{

	}

	public Rigidbody pelvis;
	protected override void ApplyPhysics()
	{
		Rigidbody[] rbs = this.GetComponentsInChildren<Rigidbody> ();
		foreach(Rigidbody rb in rbs)
		{
			rb.useGravity = true;
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
	}


	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}

}
