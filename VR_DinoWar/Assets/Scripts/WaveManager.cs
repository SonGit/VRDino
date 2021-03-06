﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour 
{

	public static WaveManager instance;

	void Awake()
	{
		instance = this;
	}

	[HideInInspector] public List<Enemy> enemyList = new List<Enemy>();


	public enum SpawnState
	{
        Stop,
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
	public float timeNextWave = 50f;
	public float nextWaveCountdown;
	public float waveCountDown;

	public SpawnState state = SpawnState.Counting;


	private int nextWave = 0;
	private int countWave;


	void Start ()
	{
        Stop();
        waveCountDown = timeBetweenWaves;
		nextWaveCountdown = timeNextWave;
	}

    public void Stop()
    {
        state = SpawnState.Stop;
    }

	void Update ()
	{
        if (state == SpawnState.Stop)
        {
            return;
        }

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
		nextWaveCountdown -= Time.deltaTime;
		if (nextWaveCountdown <= 0 || GameObject.FindGameObjectWithTag("Enemy") == null) 
		{
			nextWaveCountdown = timeNextWave;
			return false;
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
			countWave++;
			enemyList = new List<Enemy>();
			Debug.Log ("All Wave Complete!");
		} 
		else 
		{
			Debug.Log (waves[nextWave].name + " Complete!" );
			countWave++;
			enemyList = new List<Enemy>();
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

	public string GetCurrentWave ()
	{
		return waves [nextWave].name;
	}

	public void ResetWave ()
	{
		state = SpawnState.Counting;
		enemyList = new List<Enemy>();
		waveCountDown = timeBetweenWaves;
		nextWave = 0;
		countWave = 0;
	}

	public int GetEnemyIsAlive ()
	{
		int enemyCount = 0;
		foreach (Enemy item in enemyList) 
		{
			if (item.hitPoints > 0) 
			{
				enemyCount++;
			} 
		}
		return enemyCount;
	}

	public void GetEnemyCheerWorlds()
	{
		foreach (Enemy item in enemyList) 
		{
			if (item.hitPoints > 0) 
			{
				item.CheerWorlds ();
			} 
		}
	}

	public void ResetEnemy()
	{
		foreach (Enemy item in enemyList) 
		{
			if (item.hitPoints > 0) {
				item.OnHit (300);
			}
		}
	}

	public int CountWave ()
	{
		return countWave;
	}
		
}
