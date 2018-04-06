using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour 
{
	public enum SpawnState
	{
		Spawning,
		Waitting,
		Counting
	};


	[System.Serializable]
	public class Wave
	{
		public string name;
		public int count;
		public float rate;
	}

	public Wave[] waves;
	public float timeBetweenWaves = 5f;
	public float waveCountDown;
	public SpawnState state = SpawnState.Counting;


	private int nextWave = 0;
	private float searchCountdown = 1f;

	void Start ()
	{
		waveCountDown = timeBetweenWaves;
	}

	void Update ()
	{
		if (state == SpawnState.Waitting) 
		{
			if (!EnemyIsAlive ()) 
			{
				WaveCompleted ();
			} 
			else 
			{
				return;
			}
		}

		if (waveCountDown <= 0)
		{
			if (state != SpawnState.Spawning) 
			{
				StartCoroutine (SpawnWave (waves [nextWave]));
			} 
		}
		else 
		{
			waveCountDown -= Time.deltaTime;
		}
	}

	private bool EnemyIsAlive ()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0) 
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Enemy") == null) 
			{
				return false;
			}
		}
			
		return true;
	}

	private void WaveCompleted ()
	{
		state = SpawnState.Counting;
		waveCountDown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1) 
		{
			nextWave = 0;
			Debug.Log ("All Wave Complete!");
		} 
		else 
		{
			Debug.Log (waves[nextWave].name + " Complete!" );
			nextWave++;
		}
		
	}

	IEnumerator SpawnWave (Wave wave)
	{
		state = SpawnState.Spawning;
		for (int i = 0; i < wave.count; i++) {
			SpawnEnemy ();
			yield return new WaitForSeconds (wave.rate);
		}

		state = SpawnState.Waitting;

		yield break;
	} 

	void SpawnEnemy ()
	{
		Spawner.instance.SpawnEnemy ();
	}
}
