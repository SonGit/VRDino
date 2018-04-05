using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : BaseMenu {

	public static GameOverMenu instance;

	void Awake()
	{
		instance = this;
	}

	void Start () {
		Init ();
		DisableCanvas ();
	}

	public override void Show()
	{
		base.Show ();
	}
}
