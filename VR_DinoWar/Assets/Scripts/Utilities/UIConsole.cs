using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConsole : MonoBehaviour {

    public static UIConsole instance;

   
	public MainMenu menu;
    [SerializeField]
	private PlayerHUD hud;
	public GameOverMenu gameOverMenu;
	[SerializeField]
	private TutorialMenu tutorialMenu;

    // Use this for initialization
    void Awake () {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMainMenu(bool visibility)
    {
		if (visibility) {
			menu.Show ();
		} else {
			menu.Hide ();
		}
    }

    public void ShowHUD(bool visibility)
    {
        hud.gameObject.SetActive(visibility);
    }

    public void ShowGameOverMenu(bool visibility)
    {
		if (visibility) {
			gameOverMenu.Show ();
		} else {
			gameOverMenu.Hide ();
		}
    }

	public void ShowTutorialMenu(bool visibility)
	{
		if (visibility) {
			tutorialMenu.Show ();
		} else {
			tutorialMenu.Hide ();
		}

	}
}
