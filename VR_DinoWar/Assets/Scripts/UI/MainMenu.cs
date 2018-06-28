using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BaseMenu {

	public TutorialMenu tutorialMenu;

	public static MainMenu instance;

	public Collider[] colliders;

	void Awake()
	{
		instance = this;
		colliders = GetComponentsInChildren<Collider> ();
	}
	// Use this for initialization
	void Start () {
		Init ();
	}

	public void ShowTutorialMenu()
	{
		if (tutorialMenu != null) {
			tutorialMenu.Show ();
		}
			
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
