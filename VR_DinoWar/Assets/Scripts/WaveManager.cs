using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour 
{

	public static WaveManager instance;

	void Awake()
	{
		instance = this;
	}

	[HideInInspector] public List<Dino> enemyList = new List<Dino>();


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
	public float timeResetWaves = 10f;
	public float waveCountDown;
	public SpawnState state = SpawnState.Counting;


	private int nextWave = 0;
	private float searchCountdown = 1f;
	private int countWave;

	void Start ()
	{
        Stop();
        waveCountDown = timeBetweenWaves;
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
			countWave++;
			enemyList = new List<Dino>();
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
		enemyList = new List<Dino>();
		waveCountDown = timeResetWaves;
		nextWave = 0;
		countWave = 0;
	}

	public int GetEnemyIsAlive ()
	{
		int enemyCount = 0;
		foreach (Dino item in enemyList) 
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
		foreach (Dino item in enemyList) 
		{
			if (item.hitPoints > 0) 
			{
				item.CheerWorlds ();
			} 
		}
	}

	public void ResetEnemy()
	{
		foreach (Dino item in enemyList) 
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
