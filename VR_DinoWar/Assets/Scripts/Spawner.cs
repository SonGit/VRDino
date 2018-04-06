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
		Transform randSpawnPoints = spawnPoints [Random.Range (0, spawnPoints.Length)];

		Dino dino =  ObjectPool.instance.GetDinos ();
		dino.transform.position = randSpawnPoints.position;
		dino.transform.rotation = randSpawnPoints.rotation;
		dino.Live ();
	}		

}
