using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

	public Image hpImg;
	public Text hpText;
	Player playerReference;

	float maxHP;
	float barVal;

	// Use this for initialization
	void Start () {
		maxHP = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerReference == null) {
			playerReference = Player.instance;
			return;
		}

		barVal = playerReference.hitPoints / maxHP;

		if (hpImg != null) {
			hpImg.fillAmount = barVal;
		}

		if (hpText != null) {
			hpText.text = "HP " + playerReference.hitPoints + "/" + maxHP;
		}

	}
}
