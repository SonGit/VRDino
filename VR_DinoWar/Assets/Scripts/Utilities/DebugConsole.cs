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
    [SerializeField]
    private GameObject objectRadialMenu;
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
        UIConsole.instance.ShowHUD(true);
        UIConsole.instance.ShowGameOverMenu(false);
		UIConsole.instance.ShowTutorialMenu(false);

        if (player != null)
        {
            player.Reset();
        }
        else
        {
            print("player is null!");
        }

        if (waveManager != null)
        {
            waveManager.ResetEnemy();
            waveManager.ResetWave();
        }
        else
        {
            print("waveManager is null!");
        }

        ShowColliderHeadQuiver(true);
        ShowVRPointer(false);
        ShowRadialMenu(true);
    }

    public void StopGame()
    {
        print("Stop Game!");

        if (waveManager != null)
        {
            waveManager.Stop();
        }
        else
        {
            print("waveManager is null!");
        }
       
        ShowRadialMenu(false);
        ShowVRPointer(true);
        UIConsole.instance.ShowHUD(false);
    }

    public void ReturnToMainMenu()
    {
        UIConsole.instance.ShowGameOverMenu(false);
        UIConsole.instance.ShowMainMenu(true);
    }

    public void GameOver()
    {
        Inventory.instance.DropAllWeapon();

        UIConsole.instance.ShowGameOverMenu(true);

        if (waveManager != null)
        {
            waveManager.GetEnemyCheerWorlds();
            waveManager.Stop();
        }
        else
        {
            print("waveManager is null!");
        }

        ShowRadialMenu(false);
        ShowVRPointer(true);
        ShowColliderHeadQuiver(false);
    }

    void ShowRadialMenu(bool visible)
    {
        if (objectRadialMenu != null)
        {
            objectRadialMenu.SetActive(visible);
        }
        else
        {
            print("objectRadialMenu is null!");
        }
    }

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

    void ShowColliderHeadQuiver(bool visible)
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
}
