using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenu : MonoBehaviour {

	protected RectTransform container;
	protected Canvas canvas;
	protected MenuManager manager;

	// Use this for initialization
	public void Init () {
		container = this.GetComponent<RectTransform> ();
		canvas = this.GetComponent<Canvas> ();
		manager = this.GetComponentInParent<MenuManager> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public virtual void Show()
	{
		Time.timeScale = 0;
		EnableCanvas ();
		ShowEffect ();
	}

	public virtual void Hide()
	{
		HideEffect ();
	}

	protected virtual void ShowEffect ()
	{
		container.transform.position = new Vector3 (-23,17,-37.5f);
		iTween.MoveTo(gameObject,iTween.Hash("x", .25, "easetype", iTween.EaseType.easeInSine, "time", .2, "ignoretimescale", true));


	}

	protected virtual void HideEffect ()
	{
		iTween.MoveBy(gameObject,iTween.Hash(
			"y"   , 5.25f,
			"time", 1.5f,
			//"easetype",iTween.EaseType.spring,
			"oncomplete", "DisableCanvas",
			"oncompletetarget",gameObject,
			"ignoretimescale",true
		));
	}

	protected void EnableCanvas()
	{
		if (canvas != null)
			canvas.enabled = true;
	}

	protected void DisableCanvas()
	{
		if (canvas != null)
			canvas.enabled = false;
	}
}
