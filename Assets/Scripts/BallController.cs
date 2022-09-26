using System;
using UnityEngine;

namespace Pinball
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 25f;

        private Rigidbody2D rbody;

        private void Awake()
        {
            TryGetComponent<Rigidbody2D>(out rbody);
        }

        private void FixedUpdate()
        {
            ClampBallSpeed();
        }

        private void ClampBallSpeed()
        {
            float speed = rbody.velocity.magnitude;

            if (speed > maxSpeed)
            {
                float brakeSpeed = speed - maxSpeed;
                Vector2 normalizedVelocity = rbody.velocity.normalized;
                Vector2 brakeVelocity = normalizedVelocity * brakeSpeed;

                rbody.AddForce(-brakeVelocity);
            }
        }
        
        public void BallSpeedMultiplier(float value)
        {
            float speed = rbody.velocity.magnitude;
            Vector2 direction = rbody.velocity.normalized;

            Vector2 multipliedVelocity = direction * speed * value;

            rbody.velocity = multipliedVelocity;
        }
        
        public void ChangeBallDirection(Vector2 velocityVector , float rotationDegree)
        {
            float speed = velocityVector.magnitude;
            var direction = velocityVector.normalized;

            var rotatedVector = RotateVector(direction, rotationDegree);
            var velocity = rotatedVector * speed;
        
            rbody.velocity = velocity;
        }
        
        private Vector3 RotateVector(Vector2 vector, float degree) =>
            Quaternion.Euler(0, 0, degree) * vector;
    }
}