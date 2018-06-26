using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThrowRock : MonoBehaviour {
	//public Transform targ;
	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			if (Player.instance != null) {
				ThrowObject obj = ObjectPool.instance.GetRockThrow ();
				obj.transform.position = transform.position;
				//obj.transform.position = transform.position + Random.insideUnitSphere * 15;
				//obj.transform.position = new Vector3 (obj.transform.position.x,8.5f,obj.transform.position.z);
				obj.Launch (Player.instance.transform.position,transform);
			}

			yield return new WaitForSeconds (5);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
