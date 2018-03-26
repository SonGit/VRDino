using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	public static Player instance;

	public int enemyNo = 0;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void Die()
	{
		Debug.Log ("player Die");
	}

	public override void OnLive ()
	{

	}

	public override void OnDestroy ()
	{

	}

}
