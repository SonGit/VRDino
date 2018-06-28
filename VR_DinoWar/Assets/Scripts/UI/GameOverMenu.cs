using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : BaseMenu {

	public static GameOverMenu instance;

	public Collider[] colliders;

	void Awake()
	{
		instance = this;
		colliders = GetComponentsInChildren<Collider> ();
	}

	void Start () {
		Init ();
		//DisableCanvas ();
	}

	public override void Show()
	{
		base.Show ();
	}

	public void HideCollider(){
		foreach (Collider item in colliders) {
			item.enabled = false;
		}
	}

	public void ShowCollider(){
		foreach (Collider item in colliders) {
			item.enabled = true;
		}
	}
}
