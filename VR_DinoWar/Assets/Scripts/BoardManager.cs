using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardManager : MonoBehaviour {

	public static BoardManager instance;

	void Awake()
	{
		instance = this;
	}
		

	public TextMeshPro messageTextProScore;
	public TextMeshPro messageTextProEnemyIsAlive;
	public TextMeshPro messageTextProCureentWave;

	private string message;


	void Update ()
	{
		StartCoroutine (WaitShowMessage());
	}

	IEnumerator WaitShowMessage ()
	{
		yield return new WaitForSeconds (1);
		ShowMessage ();
	}

	private void ShowMessage ()
	{
		message = EndMessageScore ();
		messageTextProScore.text = message;

		message = EndMessageEnemyIsAlive ();
		messageTextProEnemyIsAlive.text = message;

		message = EndMessageCurrentWave ();
		messageTextProCureentWave.text = message;
	}

	private string EndMessageScore()
	{
		message = "SCORE: " + Player.instance.score;
		return message;
	}

	private string EndMessageCurrentWave()
	{
		message = "WAVE: " + WaveManager.instance.GetCurrentWave ();
		return message;
	}

	private string EndMessageEnemyIsAlive()
	{
		message = "IsAlive: " + WaveManager.instance.GetEnemyIsAlive ();
		return message;
	}
		
}
