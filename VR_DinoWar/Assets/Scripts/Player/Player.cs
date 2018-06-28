using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Player : Character {

	public static Player instance;

	#region weapon
	public Spear spear;
	#endregion

	#region player info
	public int enemyNo = 0;
	public int score;
	#endregion

	#region player hands
	[Tooltip("Drag Right Controller in [VRTK_scripts] here")]
	[SerializeField]
	private Transform rightHand;
	[Tooltip("Drag Left Controller in [VRTK_scripts] here")]
	[SerializeField]
	private Transform leftHand;
	//Cache Components
	private VRTK_InteractTouch RightHandTouch; 
	private VRTK_InteractGrab RightHandGrab;
	private VRTK_ControllerEvents RightHandEvents;
	private VRTK_InteractTouch LeftHandTouch;
	private VRTK_InteractGrab LeftHandGrab;
	#endregion

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		RightHandTouch = rightHand.GetComponent<VRTK_InteractTouch> ();
		RightHandGrab = rightHand.GetComponent<VRTK_InteractGrab> ();
		RightHandEvents =  rightHand.GetComponent<VRTK_ControllerEvents> ();
		LeftHandTouch = leftHand.GetComponent<VRTK_InteractTouch> ();
		LeftHandGrab = leftHand.GetComponent<VRTK_InteractGrab> ();

		RightHandEvents.TriggerPressed += new ControllerInteractionEventHandler(GrabSpear);
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
		this.enabled = true;
		spear.enabled = true;
    }


	public void GrabWeapon (GameObject weapon)
	{
		print ("GRAB");
		RightHandTouch.ForceTouch (weapon);
		RightHandGrab.AttemptGrab ();

	}

	private void GrabSpear(object sender, ControllerInteractionEventArgs e)
	{
		spear.ReturnToHand ();
	}

	public void DropAllWeapon ()
	{
		RightHandGrab.ForceRelease ();
		LeftHandGrab.ForceRelease ();
	}
		
}
