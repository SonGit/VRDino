using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace VRTK.Examples.Archery
{
	public class Grenade : MonoBehaviour {

		public static Grenade instance;
		
		public Collider blastRadius;
		public Collider colliderFight;


		private Arrow arrow;

		void Awake ()
		{
			instance = this;
		}

		// Use this for initialization
		void Start () {
			//Initialize ();
			blastRadius.enabled = false;
			colliderFight.enabled = false;
			arrow = GetComponentInParent<Arrow> ();

		}

		Enemy enemy;
		public void CheckIfEnemyAndBlast(Transform t)
		{
			enemy = t.root.GetComponent<Enemy> ();
			// If player indeed hit the enemy
			if (enemy != null) {
				enemy.Blast (transform.position);
			}

			Rigidbody rb = t.GetComponentInParent<Rigidbody> ();
			if (rb != null) {
				rb.AddExplosionForce (1000,transform.position,100);
			}
				
		}
			
		private void OnTriggerEnter (Collider collider)
		{
			if (arrow.inFlight && collider.gameObject.tag == "Terrain 1"){
				StartCoroutine (Explode (transform.position));
			}
				
			CheckIfEnemyAndBlast (collider.transform);
		}
			
			
		public IEnumerator Explode(Vector3 pos)
		{
			ExplosionBoom explosionBoom = ObjectPool.instance.GetExplosionBoom ();
			explosionBoom.transform.position = pos;
			explosionBoom.Live ();
			AudioManager.instance.PlayClip (AudioManager.SoundFX.Explosion1,transform.position,0.7f);
			blastRadius.enabled = true;
			yield return new WaitForSeconds (0.02f);
			if (gameObject != null) {
				Destroy (gameObject);
				Destroy (arrow.gameObject);
			}


		}
			
	}
}