using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pinball
{
    public class Attractor : MonoBehaviour
    {
        [SerializeField] private float waitTime = 3.0f;
        [SerializeField] private float deactivateTime = 15f;
        [SerializeField] private float minDistance;
        [SerializeField] private float pushForce = 20;
        
        private PointEffector2D effector;
        private bool isActive=true;
        private Rigidbody2D inRigidbody2D;
        

        private void Awake()
        {
            TryGetComponent<PointEffector2D>(out effector);
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.attachedRigidbody || !isActive)   return ;
            if(inRigidbody2D != null && inRigidbody2D!=other.attachedRigidbody) return;
            
            inRigidbody2D = other.attachedRigidbody;
            var distance = (transform.position - other.transform.position).magnitude;
            if (distance <= minDistance)
                StartCoroutine(ShootSequence());
        }

        private IEnumerator ShootSequence()
        {
            isActive = false;
            effector.enabled = false;
            
            FreezeObject();

            yield return new WaitForSeconds(waitTime);
            
            ShootOnRandomDirection();
            yield return new WaitForSeconds(deactivateTime);
            
            isActive = true;
            effector.enabled = true;
            
        }

        private void FreezeObject()
        {
            inRigidbody2D.isKinematic = true;
            inRigidbody2D.velocity = Vector2.zero;
            inRigidbody2D.MovePosition(transform.position);
            inRigidbody2D.drag = 0;
        }

        private void ShootOnRandomDirection()
        {
            inRigidbody2D.isKinematic = false;
            var randomDirection = Random.insideUnitCircle;
            inRigidbody2D.AddForce(randomDirection *pushForce, ForceMode2D.Impulse );

            inRigidbody2D = null;
        }
    }
}