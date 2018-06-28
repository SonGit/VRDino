using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThrowObject : Cacheable {

	#region physics vars
	[Tooltip("height arc")] 
	[SerializeField]
	private float h = 5;
	[Tooltip("Gravity")] 
	[SerializeField]
	private float gravity = -18;
	#endregion

	#region cache components
	private Rigidbody rb;
	private TrailRenderer trailRenderer;
	private Collider m_collider;
	private Enemy enemy;
	private Player player;
	#endregion

	#region private vars
	private bool hasInit;
	private Vector3 target;
	private Transform thrower;
	private bool isLaunching;
	private float timeCount;
	private Vector3 m_velocity;
	private bool hasCausedDamage;
	#endregion

	// Use this for initialization
	void Start () {
		Destroy ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isLaunching) {
			timeCount += Time.deltaTime;
			if (timeCount > .25f) {
				if (!m_collider.enabled)
					m_collider.enabled = true;
			}
		}
	}

	#region object pooling
	public override void OnDestroy ()
	{
		if (trailRenderer == null) {
			trailRenderer = this.GetComponent<TrailRenderer> ();
		} else {
			trailRenderer.Clear ();
		}

		gameObject.SetActive (false);
		isLaunching = false;
		timeCount = 0;
	}

	public override void OnLive ()
	{
		h = 5;
		hasCausedDamage = false;
		gameObject.SetActive (true);
		hasInit = false;
	}
	#endregion

	void Init()
	{
		rb = this.GetComponent<Rigidbody>();
		trailRenderer = this.GetComponent<TrailRenderer>();
		m_collider = this.GetComponent<Collider> ();
		m_collider.enabled = false;
		rb.useGravity = false;
		hasInit = true;
	}

	#region public methods
	/// <summary>
	/// Provides a target world position here to launch object in an arc
	/// </summary>
	/// <param name="target">Target.</param>
	public void Launch(Vector3 target,Transform thrower)
	{
		if(!hasInit)
		{
			Init();
		}
			

		this.target = target;
		Physics.gravity = Vector3.up * gravity;
		rb.useGravity = true;
		m_velocity = CalculateLaunchData().initialVelocity;

		// Check for bad velocity and tries to fix
		while (float.IsNaN(m_velocity.magnitude)) {
			h += 3;
			m_velocity = CalculateLaunchData().initialVelocity;
		}

		rb.velocity = m_velocity;

		this.thrower = thrower;
		trailRenderer.enabled = true;

		isLaunching = true;
	}
	/// <summary>
	/// Bounce back to origin point
	/// </summary>
	public void Bounce()
	{
		if (thrower != null) {
			rb.velocity = Vector3.zero;
			Launch(thrower.position,thrower);
		}
	}
	#endregion

	#region physics calculation
	/// <summary>
	/// Calculates the launch data based on heights and gravity input.
	/// </summary>
	/// <returns>The launch data.</returns>
	LaunchData CalculateLaunchData()
	{
		float displacementY = target.y - rb.position.y;
		Vector3 displacementXZ = new Vector3(target.x - rb.position.x, 0, target.z - rb.position.z);
		float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}
	/// <summary>
	/// Launch data.
	/// </summary>
	struct LaunchData
	{
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData(Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}
	}
	#endregion

	void OnCollisionEnter(Collision collision) 
	{
		enemy = collision.transform.root.GetComponent<Enemy> ();
		player = collision.gameObject.GetComponent<Player> ();

		if (!hasCausedDamage) {
			if (enemy != null) {
				enemy.Hit (collision.collider,collision.contacts[0].point,25);
				StartCoroutine (WaitDestroy());
				hasCausedDamage = true;
			}

			if (player != null) {
				player.OnHit (5);
				StartCoroutine (WaitDestroy());
				hasCausedDamage = true;
			}

			if (collision.gameObject.tag == "Terrain 1") {
				StartCoroutine (WaitDestroy());
				hasCausedDamage = true;
			}
		}



	}

	IEnumerator WaitDestroy()
	{
		yield return new WaitForSeconds (4f);
		SmokeEffect (transform);
		Destroy ();
	}

	private void SmokeEffect(Transform pos)
	{
		SmokeEffect smokeEffect = ObjectPool.instance.GetSmokeEffect ();
		smokeEffect.transform.position = pos.position;
		smokeEffect.Live ();
	}
}
