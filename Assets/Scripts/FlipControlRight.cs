using System;
using UnityEngine;

namespace Pinball
{
    public class FlipControlRight : MonoBehaviour
    {
        public bool isKeyPress = false;
        public bool isTouched = false;
    
        public float speed = 0f;
        private HingeJoint2D hingeJoint2D;
        private JointMotor2D jointMotor;

        private InputManager inputManager;
        void Start()
        {
            
            
            hingeJoint2D = GetComponent<HingeJoint2D>();
            jointMotor = hingeJoint2D.motor;
        }

        private void OnEnable()
        {
            inputManager = FindObjectOfType<InputManager>();
            
            inputManager.OnFlipperRightPressed += HandleFlipperRightPressed;
            inputManager.OnFlipperRightReleased += HandleFlipperRightReleased;
        }

        private void OnDisable()
        {
            inputManager.OnFlipperRightPressed -= HandleFlipperRightPressed;
            inputManager.OnFlipperRightReleased -= HandleFlipperRightReleased;
        }

        private void HandleFlipperRightPressed()
        {
            isKeyPress = true;
        }

        private void HandleFlipperRightReleased()
        {
            isKeyPress = false;
        }
        void FixedUpdate()
        {
            // on press keyboard or touch Screen
            if (isKeyPress == true && isTouched == false || isKeyPress == false && isTouched == true)
            {
                // set motor speed to max
                jointMotor.motorSpeed = -speed;
                hingeJoint2D.motor = jointMotor;
            }
            else
            {
                // snap the motor back again
                jointMotor.motorSpeed = speed;
                hingeJoint2D.motor = jointMotor;
            }
        }
    }
}