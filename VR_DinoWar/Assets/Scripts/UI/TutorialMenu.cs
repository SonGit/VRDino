using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : BaseMenu {
	
	public MainMenu mainMenu;

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

}
