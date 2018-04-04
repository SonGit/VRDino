﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour {

	RectTransform container;
	Canvas canvas;
	// Use this for initialization
	IEnumerator Start () {
		container = this.GetComponent<RectTransform> ();
		canvas = this.GetComponent<Canvas> ();
		DisableCanvas ();
		Show ();

		yield return new WaitForSeconds (2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Show()
	{
		EnableCanvas ();
		container.transform.position = new Vector3 (-25,17,-37.5f);
		iTween.MoveBy(gameObject,iTween.Hash(
			"y"   , -5.25f,
			"time", 1.5f,
			"easetype",iTween.EaseType.spring
		));
	}

	public void Hide()
	{
		iTween.MoveBy(gameObject,iTween.Hash(
			"y"   , 5.25f,
			"time", 1.5f,
			"easetype",iTween.EaseType.spring,
			"oncomplete", "DisableCanvas",
			"oncompletetarget",gameObject
		));
	}
		
	void EnableCanvas()
	{
		if (canvas != null)
			canvas.enabled = true;
	}

	void DisableCanvas()
	{
		if (canvas != null)
			canvas.enabled = false;
	}
}
