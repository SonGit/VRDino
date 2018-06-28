using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : BaseMenu {

	public static TutorialMenu instance;

	public MainMenu mainMenu;

	public Collider[] colliders;

	void Awake()
	{
		instance = this;
		colliders = GetComponentsInChildren<Collider> ();
	}

	// Use this for initialization
	void Start () {
		Init ();
		//DisableCanvas ();
	}

	public void BackToMainMenu()
	{
		if(mainMenu != null)
		mainMenu.Show ();
		Hide ();
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
