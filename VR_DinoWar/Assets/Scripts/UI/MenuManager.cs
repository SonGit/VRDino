using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public BaseMenu[] menus;

	// Use this for initialization
	void Start () {
		menus = this.GetComponentsInChildren<BaseMenu> ();
	}
	
	public void CloseOtherMenus(BaseMenu openedMenu)
	{
		foreach (BaseMenu menu in menus) {
			if (menu != openedMenu) {
				menu.Hide ();
			} 
		}
	}
		
}
