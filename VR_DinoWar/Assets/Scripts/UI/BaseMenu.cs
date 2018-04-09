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
		EnableCanvas ();
		ShowEffect ();
		PlayMenuClickSound ();
	}

	public virtual void Hide()
	{
		HideEffect ();
		PlayMenuClickSound ();
	}

	protected virtual void ShowEffect ()
	{
		container.transform.position = new Vector3 (-23f,17.5f,-37.25f);
		iTween.MoveBy(gameObject,iTween.Hash(
			"y"   , -5.25f,
			"time", 1.5f
		));
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

	private void PlayMenuClickSound ()
	{
		AudioManager.instance.PlayClip (AudioManager.SoundFX.MenuClick,transform.position);
	}
}
