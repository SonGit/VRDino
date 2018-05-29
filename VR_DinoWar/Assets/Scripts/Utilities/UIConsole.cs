using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConsole : MonoBehaviour {

    public static UIConsole instance;

    [SerializeField]
    private MainMenu menu;
    [SerializeField]
    private PlayerHUD hud;
    [SerializeField]
    private GameOverMenu gameOverMenu;

    // Use this for initialization
    void Awake () {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMainMenu(bool visibility)
    {
        menu.gameObject.SetActive(visibility);
    }

    public void ShowHUD(bool visibility)
    {
        hud.gameObject.SetActive(visibility);
    }

    public void ShowGameOverMenu(bool visibility)
    {
        gameOverMenu.gameObject.SetActive(visibility);
    }
}
