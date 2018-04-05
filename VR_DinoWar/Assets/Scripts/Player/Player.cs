using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.SceneManagement;

public class Player : Character {

	public static Player instance;

	public int enemyNo = 0;
	public GameObject objectWaveManager;
	public GameObject objectRightController;
	public GameObject objectRadialMenu;
	public GameObject objectHUD;
	public GameObject objectHeadQuiver; 

	[HideInInspector] public int score;

	private WaveManager waveManager;
	private VRTK_StraightPointerRenderer VRTK_straightPointerRenderer;
	private BoxCollider colliderHeadQuiver;

	void Awake()
	{
		instance = this;
		waveManager = objectWaveManager.GetComponent<WaveManager> ();
		VRTK_straightPointerRenderer = objectRightController.GetComponent<VRTK_StraightPointerRenderer> ();
		colliderHeadQuiver = objectHeadQuiver.GetComponent<BoxCollider> ();
	}

	// Use this for initialization
	void Start () {
		StopGame ();
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
		GameOver ();
	}

	public override void OnLive ()
	{

	}

	public override void OnDestroy ()
	{

	}

	public void StartGame()
	{
		waveManager.enabled = true;
		MainMenu.instance.Hide ();
		objectRadialMenu.SetActive (true);
		VRTK_straightPointerRenderer.enabled = false;
		objectHUD.SetActive (true);
	}

	public void StopGame()
	{
		waveManager.enabled = false;
		objectRadialMenu.SetActive (false);
		VRTK_straightPointerRenderer.enabled = true;
		objectHUD.SetActive (false);
	}

	public void ResetGame()
	{
		Player.instance.hitPoints = 100;
		WaveManager.instance.ResetEnemy ();
		Player.instance.score = 0;
		WaveManager.instance.ResetWave ();
		GameOverMenu.instance.Hide ();
		waveManager.enabled = true;
		objectRadialMenu.SetActive (true);
		VRTK_straightPointerRenderer.enabled = false;
		colliderHeadQuiver.enabled = true;
	}

	public void ReturnGame()
	{
		SceneManager.LoadScene ("VR_DinoWar_Enviroment");
	}

	public void GameOver()
	{
		GameOverMenu.instance.Show ();
		WaveManager.instance.GetEnemyCheerWorlds ();
		objectRadialMenu.SetActive (false);
		VRTK_straightPointerRenderer.enabled = true;
		waveManager.enabled = false;
		Inventory.instance.DropAllWeapon ();
		colliderHeadQuiver.enabled = false;
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

}
