using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	
	public static Spawner instance;

	void Awake()
	{
		instance = this;
	}

	IEnumerator Start () {

		while (true) {
			Dino dino =  ObjectPool.instance.GetDinos ();
			dino.transform.position = transform.position;
			dino.Live();

			yield return new WaitForSeconds (Random.Range(5,10));
		}


	}

	public Vector3 GetRandomSpawner()
	{
		string tag = "Spawner";

		GameObject[] SpawnerObjs = GameObject.FindGameObjectsWithTag (tag);
		int randomSpawner = Random.Range (0,SpawnerObjs.Length);

		return SpawnerObjs[randomSpawner].transform.position;
	}
	

}
