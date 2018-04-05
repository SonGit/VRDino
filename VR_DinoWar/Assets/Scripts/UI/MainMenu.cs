using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BaseMenu {

	public TutorialMenu tutorialMenu;

	public static MainMenu instance;

	void Awake()
	{
		instance = this;
	}
	// Use this for initialization
	void Start () {
		Init ();
	}

	public void ShowTutorialMenu()
	{
		if (tutorialMenu != null)
			tutorialMenu.Show ();
		Hide ();
	}
}
