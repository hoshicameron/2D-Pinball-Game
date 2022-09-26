using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pinball
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class BlackHole : MonoBehaviour
    {    
    
        [SerializeField] private float gravityPull    = 7000.0f;
        [SerializeField] private float pushForce = 1000f;
        
        [SerializeField] private float distanceTOCenter = 0.2f;
        [SerializeField] private float waitTime = 3.0f;
        [SerializeField] private float deactivateTime = 15.0f;
    
    
        private List<Rigidbody2D> rigidBodies = new List<Rigidbody2D>();

        private Rigidbody2D rigidbody;
        private Collider2D collider;
        private bool deactivate = false;

        private void Awake()
        {
            TryGetComponent<Collider2D>(out collider);
            TryGetComponent<Rigidbody2D>(out rigidbody);
        }
        private void FixedUpdate()
        {    
            if(deactivate)  return;
        
            UpdateBlackHole();
        }
        void OnTriggerEnter2D(Collider2D col)
        {
            if ( col.attachedRigidbody != null && rigidBodies != null )
            {
                col.attachedRigidbody.velocity = Vector2.zero;
                rigidBodies.Add( col.attachedRigidbody );
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (rigidBodies.Contains(col.attachedRigidbody))
            {
                rigidBodies.Remove(col.attachedRigidbody);
            }
        }

        private void UpdateBlackHole()
        {
            if (rigidBodies == null) return;
        
            for (int i = 0; i < rigidBodies.Count; i++)
            {
                if( rigidBodies[i] != null )
                {
                    CalculateMovement( rigidBodies[i] );
                }
            }
        }

    

        private void CalculateMovement(Rigidbody2D rBody)
        {
            var direction = transform.position - rBody.transform.position;
            float distance = direction.magnitude;
            
            if (distance > distanceTOCenter)
            {
                Suction(rBody, distance, direction);
            } else
            {
                StartCoroutine(HoldBall(rBody));
            }
        }

        private void Suction(Rigidbody2D rBody, float distance, Vector3 direction)
        {
            float forceMagnitude = gravityPull * (rigidbody.mass * rBody.mass) / Mathf.Pow(distance, 2);
            Vector3 force = direction.normalized * forceMagnitude;
            rBody.AddForce(force * Time.fixedDeltaTime);
        }

        private IEnumerator HoldBall(Rigidbody2D rBody)
        {
            deactivate = true;
            collider.enabled = false;
        
            FixRigidbody(rBody);
            yield return new WaitForSeconds(waitTime);
            ShootOnRandomDirection(rBody);
        
        
            yield return new WaitForSeconds(deactivateTime);
            collider.enabled = true;
            deactivate = false;

        }

        private void ShootOnRandomDirection(Rigidbody2D rBody)
        {
            rBody.isKinematic = false;
            var randomDirection = Random.insideUnitCircle.normalized;
            rBody.AddForce(randomDirection *pushForce, ForceMode2D.Impulse );

            rigidBodies.Remove(rBody);
        }

        private void FixRigidbody(Rigidbody2D rBody)
        {
            rBody.velocity = Vector2.zero;
            rBody.isKinematic = true;
            rBody.MovePosition(transform.position);
            rBody.drag = 0;
        }

    }
}