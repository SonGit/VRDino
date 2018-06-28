using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DebugConsole : MonoBehaviour
{
	public static DebugConsole instance;


    [SerializeField]
	private WaveManager waveManager;
    [SerializeField]
	private VRTK_StraightPointerRenderer VRTK_straightPointerRenderer;
    [SerializeField]
	private BoxCollider colliderHeadQuiver;

	//private GameObject objectRadialMenu;
    [SerializeField]
	private Player player;

	void Awake ()
	{
		instance = this;
	}

    void Start()
    {
        StopGame();
    }

    public void StartGame()
    {
        print("Start Game!");
        UIConsole.instance.ShowMainMenu(false);
        //UIConsole.instance.ShowHUD(true);
        UIConsole.instance.ShowGameOverMenu(false);
		UIConsole.instance.ShowTutorialMenu(false);

        if (waveManager != null)
        {
            waveManager.ResetEnemy();
            waveManager.ResetWave();
        }
        else
        {
            print("waveManager is null!");
        }

		if (player != null)
		{
			player.Reset();
		}
		else
		{
			print("player is null!");
		}

        //ShowColliderHeadQuiver(true);
        ShowVRPointer(false);
        //ShowRadialMenu(true);
    }

    public void StopGame()
    {
        print("Stop Game!");

		if (player != null)
		{
			player.GetComponent<Player> ().enabled = false;
			player.spear.enabled = false;
		}
		else
		{
			print("player is null!");
		}

        if (waveManager != null)
        {
            waveManager.Stop();
        }
        else
        {
            print("waveManager is null!");
        }
       
        //ShowRadialMenu(false);
        ShowVRPointer(true);
        UIConsole.instance.ShowHUD(false);
		UIConsole.instance.ShowGameOverMenu(false);
		UIConsole.instance.ShowTutorialMenu(false);
    }

    public void ReturnToMainMenu()
    {
        UIConsole.instance.ShowGameOverMenu(false);
		UIConsole.instance.ShowTutorialMenu(false);
        UIConsole.instance.ShowMainMenu(true);
    }


	public void ToTutorial()
	{
		UIConsole.instance.ShowTutorialMenu(true);
		UIConsole.instance.ShowMainMenu(false);
	}

	public void ToMainMenu()
	{
		UIConsole.instance.ShowMainMenu(true);
		UIConsole.instance.ShowGameOverMenu(false);
		UIConsole.instance.ShowTutorialMenu(false);

		if (waveManager != null)
		{
			waveManager.ResetEnemy();
			waveManager.ResetWave();
		}
		else
		{
			print("waveManager is null!");
		}
	}

    public void GameOver()
    {
		
        UIConsole.instance.ShowGameOverMenu(true);

		if (player != null)
		{
			player.GetComponent<Player> ().enabled = false;
			player.spear.enabled = false;
			player.DropAllWeapon();
		}
		else
		{
			print("player is null!");
		}

		StopwaveManager ();


        //ShowRadialMenu(false);
        ShowVRPointer(true);
        ShowColliderHeadQuiver(false);
    }

//    void ShowRadialMenu(bool visible)
//    {
//        if (objectRadialMenu != null)
//        {
//            objectRadialMenu.SetActive(visible);
//        }
//        else
//        {
//            print("objectRadialMenu is null!");
//        }
//    }

    void ShowVRPointer(bool visible)
    {
        if (VRTK_straightPointerRenderer != null)
        {
            VRTK_straightPointerRenderer.enabled = visible;
        }
        else
        {
            print("VRTK_straightPointerRenderer is null!");
        }
    }

	public void ShowColliderHeadQuiver(bool visible)
    {
        if (colliderHeadQuiver != null)
        {
            colliderHeadQuiver.enabled = visible;
        }
        else
        {
            print("colliderHeadQuiver is null!");
        }
    }

	private void StopwaveManager (){
		if (waveManager != null)
		{
			waveManager.GetEnemyCheerWorlds();
			waveManager.Stop();
		}
		else
		{
			print("waveManager is null!");
		}
	}
}
