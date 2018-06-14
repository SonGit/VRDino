using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Player : Character {

	public static Player instance;

	public int enemyNo = 0;

	public int score;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.U)) 
		{
			OnHit (100);
		}
	}

	protected override void Die()
	{
		Debug.Log ("player Die");
		DebugConsole.instance.GameOver ();
	}

	public override void OnLive ()
	{

	}

	public override void OnDestroy ()
	{

	}

	public void ExitGame()
	{
		Application.Quit ();
	}

	public void ReduceEngagedEnemy()
	{
		enemyNo--;

		if (enemyNo < 0)
			enemyNo = 0;
	}

	public void IncreaseEngagedEnemy()
	{
		enemyNo++;

		if (enemyNo > Utility.MAX_ATTK_ENEMY)
			enemyNo = Utility.MAX_ATTK_ENEMY;
	}

	public int GetCurrentEngagedEnemy()
	{
		return enemyNo;
	}

    public void Reset()
    {
        hitPoints = 100;
        score = 0;
    }

}
