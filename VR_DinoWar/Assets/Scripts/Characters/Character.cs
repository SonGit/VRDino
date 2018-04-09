using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Cacheable {

	public int hitPoints;

	public void OnHit(int dmg)
	{
		if (hitPoints <= 0)
			return;
		else {
			hitPoints -= dmg;

			if (hitPoints <= 0) {
				Die ();
			}
		}
	}

	protected virtual void Die()
	{
		

	}



}
