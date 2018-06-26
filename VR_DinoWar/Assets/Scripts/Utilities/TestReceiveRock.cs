using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReceiveRock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo(gameObject, iTween.Hash("position", Vector3.zero, "time", 21, "easeType", iTween.EaseType.linear));
	}
	ThrowObject throwObj;
	protected virtual void OnCollisionEnter(Collision collision)
	{
		throwObj = collision.gameObject.GetComponent<ThrowObject> ();
		// If player indeed hit the enemy
		if (throwObj != null) {
			throwObj.Bounce ();
		}
	}
}
