using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;

	void Awake()
	{
		instance = this;
	}

	public int GetScore (int score)
	{
		return Player.instance.score += score;
	}
		
}
