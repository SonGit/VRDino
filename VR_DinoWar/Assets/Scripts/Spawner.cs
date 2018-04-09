using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	
	public static Spawner instance;

	void Awake()
	{
		instance = this;
	}

	public Transform[] spawnPoints;
		
	public void SpawnEnemy () {
		StartCoroutine (Spawn());
	}	

	IEnumerator Spawn()
	{
		Transform randSpawnPoints = spawnPoints [Random.Range (0, spawnPoints.Length)];

		Dino dino =  ObjectPool.instance.GetDinos ();
		yield return new WaitForSeconds (.5f);
		dino.transform.position = randSpawnPoints.position;
		dino.Live ();
		WaveManager.instance.enemyList.Add(dino);
	}

}
