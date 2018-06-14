using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	
	public static Spawner instance;
	public Dino dino;
	public Dino_LongLeg dino_LongLeg;
	public GameObject[] spawnPoints;

	void Awake()
	{
		instance = this;
	}

	void Start ()
	{
		spawnPoints = GameObject.FindGameObjectsWithTag ("Spawner");
	}
		
	public void SpawnEnemy () {
		StartCoroutine (Spawn(dino));
		StartCoroutine (Spawn(dino_LongLeg));
	}	

	IEnumerator Spawn(Enemy enemy)
	{
		GameObject randSpawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
		enemy =  ObjectPool.instance.GetEnemy (enemy);
		yield return new WaitForSeconds (.5f);
		enemy.transform.position = randSpawnPoint.transform.position;
		enemy.Live ();
		WaveManager.instance.enemyList.Add(enemy);
	}

}
