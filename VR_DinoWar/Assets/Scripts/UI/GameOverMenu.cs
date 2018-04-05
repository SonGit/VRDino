using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : TutorialMenu {

	IEnumerator Start () {
		container = this.GetComponent<RectTransform> ();
		canvas = this.GetComponent<Canvas> ();
		DisableCanvas ();
		Show ();

		yield return new WaitForSeconds (2);
	}
}
