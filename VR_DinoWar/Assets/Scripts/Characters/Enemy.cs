using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RootMotion.Dynamics;
using RootMotion.FinalIK;

public abstract class Enemy : Character {

	#region caches for profiency
	private HitReaction hitReaction;
	public FullBodyBipedIK bodyIK;
	[SerializeField]
	private Transform puppet;
	[SerializeField]
	private Transform stunEffectPosition; // place where stun fx spawn
	[SerializeField]
	private Transform smokeEffectPosition;// place where smoke FX spawn
	#endregion

	#region sounds vars
	public AudioSource footstepSFX;
	#endregion

	#region publics vars
	[HideInInspector] public Animator animator;
	[HideInInspector] public NavMeshAgent agent;
	[HideInInspector] public float initialSpeed; 
	[HideInInspector] public NavMeshObstacle obs;
	[HideInInspector] public bool isIdleDone;
	public StateController stateController;
	public float countAttack; 
	public float rate;
	public bool isAttack;
	public bool isGetScore;
	#endregion

	#region events
	public delegate void OnIdleAnimOverEvent (StateController controller);
	public OnIdleAnimOverEvent onIdleAnimDone;

	public delegate void OnEndAttackAnim (StateController controller);
	public OnEndAttackAnim onEndAttackAnim;
	#endregion

	public bool isJumping;

	//Collider[] colliders;

	//EnemyGrab enemyGrab;

	/// <summary>
	/// Put this in Start() to init vars
	/// </summary>
	public void Initialize()
	{
		animator = this.GetComponent<Animator> ();
		agent = this.GetComponentInChildren<NavMeshAgent> ();
		stateController = this.GetComponentInChildren<StateController> ();
		obs = this.GetComponentInChildren<NavMeshObstacle> ();
		//enemyGrab = this.GetComponent<EnemyGrab> ();
		hitReaction= this.GetComponentInChildren<HitReaction>();
		bodyIK = this.GetComponent<FullBodyBipedIK> ();

		initialSpeed = agent.speed;
		animator.enabled = true;
		stateController.enabled = true;
		agent.enabled = true;
		bodyIK.enabled = true;
		stateController.Default ();

		obs.enabled = false;
		hitPoints = 250;

		Rigidbody[] rbs = this.GetComponentsInChildren<Rigidbody> ();
		foreach(Rigidbody rb in rbs)
		{
			rb.useGravity = false;
			rb.isKinematic = false;
			rb.constraints = RigidbodyConstraints.FreezeRotation;

			if (rb.name == "DinolonglegLLegAnkle" || rb.name == "DinolonglegLLegPlatform" || rb.name == "DinolonglegRLegAnkle" || rb.name == "DinolonglegRLegPlatform" || rb.name == "Elbow_L" || rb.name == "DinolonglegRLegPlatform" || rb.name == "Elbow_R" || rb.name == "UpperArm" || rb.name == "ForeArm" || rb.name == "Hand" ) {
				rb.isKinematic = true;
			}
		}
			
		//colliders = this.GetComponentsInChildren<Collider> ();
		//OnOffCollider (true,true);
	}

	/// <summary>
	/// Put this in Update()
	/// </summary>
	protected void Loop()
	{
		TrackHitFreq ();

		if (stunTime > 0) {
			stunTime -= Time.deltaTime;
			if (stunTime <= 0) {
				stateController.Resume ();
				stunEffect.Destroy ();
				stunEffect = null;
			}
		}

		if (agent.velocity.magnitude > 0) {
			if (!footstepSFX.isPlaying)
				footstepSFX.Play ();
		} else {
			if (footstepSFX.isPlaying)
				footstepSFX.Stop ();
		}


		if (transform.position.y < -1f) {
			OnHit (500);
		}
	}
		
	/// <summary>
	/// Call this to hit enemy and play hit reactions based on collision point.
	/// impact determines damage.
	/// </summary>
	public void Hit(Collider hitCollider,Vector3 collisionPoint,float impact)
	{
		if (!isHit) {
			
			isHit = true;

			int damage = 5 * (int)impact + Random.Range(-10,10);

			// Run hit animation
			Vector3 dir = hitCollider.transform.position - collisionPoint;
			hitReaction.Hit (hitCollider,dir.normalized * impact/5 ,collisionPoint);

			// Show hit number pop up
			if (hitPoints > 0) 
			{
				ShowHitNumber (damage);

				// Calculate damage
				OnHit (damage);

				// Play Effect 
				HitEffect(collisionPoint);

				RandomHitSound ();

				print ("damage " + damage + " impact "+impact);
			}

		}

	}

	public float stunTime = 0;

	/// <summary>
	/// Stun this enemy. Currently not used.
	/// </summary>
	public void Stun()
	{
		stunTime += 1;
		stateController.AIEnabled = false;
		agent.enabled = false;
		animator.SetInteger ("State", 8);
		StunEffect (stunEffectPosition);
	}

	#region animation events
	// Call by animation event
	public void StartAttack()
	{
		
	}

	// Call by animation event
	public void EndAttack()
	{
		float distance;
		distance = Vector3.Distance(transform.position,stateController.playerReference.transform.position);
		if (distance < 5) 
		{
			Player.instance.OnHit (0);
		}

		isAttack = false;
	}
	#endregion

	// Call by animation event
	public void EndIdle()
	{
		isIdleDone = true;
		onIdleAnimDone (stateController);
	}

	IEnumerator WaitAnimationCheer ()
	{
		yield return new WaitForSeconds (1);
		animator.SetInteger ("State", 5);
	}

	//Force enemy to follow a pre-defined path 
	// Not used
	public void Pathing()
	{
		if(agent.isOnNavMesh)
		agent.isStopped = true;

		int rand = Random.Range (1,6);;
		string pathName = "path" + rand;

		iTween.MoveTo(gameObject, 
			iTween.Hash("path", iTweenPath.GetPath(pathName), 
				"orienttopath", true, 
				"looktime", 0.001f, 
				"lookahead", 0.001f, 
				"speed", 90, 
				"easetype", iTween.EaseType.linear, 
				"oncomplete", "OnCompletePath"));
	}

	void OnCompletePath()
	{
		
	}

	/// <summary>
	/// Orient enemy to a direction
	/// </summary>
	/// <param name="destination">Destination.</param>
	public void FaceTarget(Vector3 destination)
	{
		Vector3 lookPos = destination - transform.position;
		lookPos.y = 0;
		Quaternion rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 2);  
	}
		
	/// <summary>
	/// On enemy killed
	/// </summary>
	protected override void Die()
	{
		if (isGetScore) {
			ScoreManager.instance.GetScore (10);
		}

		Player.instance.ReduceEngagedEnemy ();
		stateController.enabled = false;
		obs.enabled = false;
		agent.enabled = false;
		RandomDieSound ();
		DieEffect();
		ApplyPhysics ();
		StartCoroutine(WaitDestroy ());
		//OnOffCollider (false,true);
		//enemyGrab.ApplyPhysicsGrab ();
	}

	/// <summary>
	/// Time before an enemy is gone to pool
	/// </summary>
	IEnumerator WaitDestroy()
	{
		yield return new WaitForSeconds (10);
		SmokeEffect (smokeEffectPosition);
		Destroy ();
	}

	protected virtual void ApplyPhysics()
	{

	}
	/// <summary>
	/// Not used
	/// </summary>
	/// <param name="center">Center.</param>
	public void Blast(Vector3 center)
	{
		if (hitPoints > 0) {
			OnHit (250);
			// Show hit number pop up
			//ShowHitNumber (50);
		}

	}

	#region local avoidance	
	public void LocalAvoidanceOn()
	{
		agent.enabled = false;
		obs.enabled = false;
	}

	public void LocalAvoidanceOff()
	{
		agent.enabled = true;
		obs.enabled = false;
	}
	#endregion

	#region collider fixes
	float hitTimecount = 0;
	float hitFreq = .15f;
	bool isHit;

	/// <summary>
	/// Prevent enemy from register every collider hit
	/// </summary>
	void TrackHitFreq()
	{
		if (isHit) {
			hitTimecount += Time.deltaTime;

			if (hitTimecount > hitFreq) {
				hitTimecount = 0;
				isHit = false;
			}

		}
	}
	#endregion

	#region misc effects
	void ShowHitNumber(int damage)
	{
		HitNumber hitNumber = ObjectPool.instance.GetHitNumber ();
		if (hitNumber != null) {
			hitNumber.transform.position = hitReaction.transform.position;
			hitNumber.transform.position += Random.insideUnitSphere * .25f;
			hitNumber.Show (damage);
		}
	}

	public void HitEffect(Vector3 pos)
	{
		TextHitRandom hitRand = ObjectPool.instance.GetTextHitRandom ();
		if (hitRand != null) {
			hitRand.transform.position = pos;
			hitRand.Live ();
		}
	}

	protected void DieEffect()
	{
		DeathSkull ds = ObjectPool.instance.GetDeathSkulls ();
		if (ds != null) {
			ds.transform.position = puppet.transform.position;
			ds.Live ();
		}
	}

	StunEffect stunEffect;

	protected void StunEffect(Transform pos)
	{
		if (stunEffect != null)
			return;
		
		stunEffect = ObjectPool.instance.GetStunEffect ();
		stunEffect.transform.position = pos.position;
		stunEffect.Live ();
	}

	protected void SmokeEffect(Transform pos)
	{
		SmokeEffect smokeEffect = ObjectPool.instance.GetSmokeEffect ();
		smokeEffect.transform.position = pos.position;
		smokeEffect.Live ();
	}

	public void CheerWorlds ()
	{
		animator.SetTrigger ("CheerTrigger");
		stateController.AIEnabled = false;
		agent.enabled = false;
	}
	#endregion

	public abstract void RandomHitSound ();

	public abstract void RandomDieSound ();

	protected virtual void Default()
	{

	}


//	public void OnOffCollider (bool boolTrigger, bool boolCollider)
//	{
//		for (int i = 0; i < colliders.Length; i++) {
//			
//			if (i == 0) {
//				colliders [i].enabled = boolTrigger;
//			} else {
//				colliders [i].enabled = boolCollider;
//			}
//		}
//	}


	Enemy enemy;
	public bool CheckIfAPlayer(Transform targetTransform)
	{
		enemy = targetTransform.GetComponent<Enemy> ();

		if (enemy != null)
			return true;

		return false;
	}


}
