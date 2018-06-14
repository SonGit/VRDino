using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino_LongLeg : Enemy {

	// Use this for initialization
	IEnumerator Start () {
		Destroy ();
        Initialize();
        Rigidbody[] rbs = this.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
        }

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

        if (agent.isOnOffMeshLink) {

            if(!isJumping)
            {
                agent.speed = 0;
				animator.SetInteger("State",0);
                stateController.AIEnabled = false;
                print(agent.currentOffMeshLinkData.endPos);
                isJumping = true;
                animator.SetTrigger("JumpTrigger");
                OffMeshEndPos = agent.currentOffMeshLinkData.endPos;
                agent.enabled = false;
            }

        }

    }

    private Vector3 OffMeshEndPos;
    // Call by animation event
    public void StartJump()
    {
        float lobTime = .7f;

        transform.LookAt(OffMeshEndPos);
        iTween.MoveTo(gameObject, iTween.Hash("position", OffMeshEndPos, "time", lobTime, "easeType", iTween.EaseType.linear));
    }

    // Call by animation event
    public void MidJump()
    {

    }

    // Call by animation event
    public void EndJump()
    {
        StartCoroutine(JumpTouchUp());
    }

    IEnumerator JumpTouchUp()
    {
        animator.SetInteger("State", 0);
        agent.enabled = false;
        yield return new WaitForSeconds(.5f);
		isJumping = false;
        agent.speed = initialSpeed; //resume speed
        agent.enabled = true;
        stateController.Resume();
    }

    protected override void ApplyPhysics()
	{
		Rigidbody[] rbs = this.GetComponentsInChildren<Rigidbody> ();
		foreach(Rigidbody rb in rbs)
		{
			rb.useGravity = true;

			float rand = Random.Range (5f,8f);
			Vector3 v = rb.velocity;

			if (rb.velocity.y > 10f) {
				v.y = rand;
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
	}


	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}
		
		
}
