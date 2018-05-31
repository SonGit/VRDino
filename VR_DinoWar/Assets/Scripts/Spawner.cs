using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	
	public static Spawner instance;
	public Dino dino;
	public Dino_LongLeg dino_LongLeg;

	void Awake()
	{
		instance = this;
	}

	public Transform[] spawnPoints;
		
	public void SpawnEnemy () {
		StartCoroutine (Spawn(dino));
		StartCoroutine (Spawn(dino_LongLeg));
	}	

	IEnumerator Spawn(Enemy enemy)
	{
		Transform randSpawnPoint_dino = spawnPoints [Random.Range (0, spawnPoints.Length)];
		enemy =  ObjectPool.instance.GetEnemy (enemy);
		yield return new WaitForSeconds (.5f);
		enemy.transform.position = randSpawnPoint_dino.position;
		enemy.Live ();
		WaveManager.instance.enemyList.Add(enemy);
	}

}
