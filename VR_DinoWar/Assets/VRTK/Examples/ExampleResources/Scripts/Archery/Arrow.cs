﻿namespace VRTK.Examples.Archery
{
    using UnityEngine;

    public class Arrow : MonoBehaviour
    {
        public float maxArrowLife = 10f;
        [HideInInspector]
        public bool inFlight = false;

        private bool collided = false;
        private Rigidbody rigidBody;
        private GameObject arrowHolder;
        private Vector3 originalPosition;
        private Quaternion originalRotation;
        private Vector3 originalScale;
		private TrailRenderer objTrailRenderer;

        public void SetArrowHolder(GameObject holder)
        {
            arrowHolder = holder;
            arrowHolder.SetActive(false);
        }

        public void OnNock()
        {
            collided = false;
            inFlight = false;
        }

        public void Fired()
        {
			objTrailRenderer.enabled = true;
            DestroyArrow(maxArrowLife);
        }

        public void ResetArrow()
        {
            collided = true;
            inFlight = false;
            RecreateNotch();
            ResetTransform();
        }

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            SetOrigns();
			objTrailRenderer = this.GetComponent<TrailRenderer> ();
        }

        private void SetOrigns()
        {
            originalPosition = transform.localPosition;
            originalRotation = transform.localRotation;
            originalScale = transform.localScale;
        }

        private void FixedUpdate()
        {
            if (!collided)
            {
                transform.LookAt(transform.position + rigidBody.velocity);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (inFlight)
            {
                ResetArrow();
            }
			objTrailRenderer.enabled = false;
			CheckIfEnemyAndDealDamage (collision,collision.contacts[0].point,10);
			// Stop spear dead on its track
			rigidBody.velocity = Vector3.zero;
			rigidBody.isKinematic = true;		
			transform.parent = collision.transform;
        }

        private void RecreateNotch()
        {
            //swap the arrow holder to be the parent again
            arrowHolder.transform.SetParent(null);
            arrowHolder.SetActive(true);

            //make the arrow a child of the holder again
            transform.SetParent(arrowHolder.transform);

            //reset the state of the rigidbodies and colliders
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
            arrowHolder.GetComponent<Rigidbody>().isKinematic = false;
        }

        private void ResetTransform()
        {
            arrowHolder.transform.position = transform.position;
            arrowHolder.transform.rotation = transform.rotation;
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
            transform.localScale = originalScale;
        }

        private void DestroyArrow(float time)
        {
            Destroy(arrowHolder, time);
            Destroy(gameObject, time);
        }

		Enemy enemy;
		protected void CheckIfEnemyAndDealDamage(Collision collision,Vector3 collisionPoint,float force)
		{
			enemy = collision.transform.root.GetComponent<Enemy> ();
			// If player indeed hit the enemy
			if (enemy != null) {
				enemy.Hit (collision.collider,collisionPoint,force * 5f);
				print (collision.collider + "        "+collisionPoint);
			}
		}



    }
}