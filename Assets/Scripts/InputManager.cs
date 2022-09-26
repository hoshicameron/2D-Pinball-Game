using System;
using UnityEngine;
using UnityEngine.Events;

namespace Pinball
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private KeyCode flipperLeft, flipperRight,menuKey;
        
        public Action OnFlipperLeftPressed, OnFlipperRightPressed, 
            OnFlipperLeftReleased, OnFlipperRightReleased;

        public UnityEvent OnMenuKeyPressed;

        public void Update()
        {
            if (Time.timeScale > 0)
            {
                GetFlipperLeftInput();
                GetFlipperRightInput();
            }

            GetMenuInput();
        }

        private void GetMenuInput()
        {
            if(Input.GetKeyDown(menuKey))
                OnMenuKeyPressed?.Invoke();
        }

        private void GetFlipperRightInput()
        {
            if(Input.GetKeyDown(flipperRight))
                OnFlipperRightPressed?.Invoke();
            if(Input.GetKeyUp(flipperRight))
                OnFlipperRightReleased?.Invoke();
        }
        private void GetFlipperLeftInput()
        {
            if(Input.GetKeyDown(flipperLeft))
                OnFlipperLeftPressed?.Invoke();
            if(Input.GetKeyUp(flipperLeft))
                OnFlipperLeftReleased?.Invoke();
        }

        public void LeftSideTouchOn()=>OnFlipperLeftPressed?.Invoke();
        public void LeftSideTouchOff()=> OnFlipperLeftReleased?.Invoke();

        public void RightSideTouchOn() => OnFlipperRightPressed?.Invoke();
        public void RightSideTouchOff() => OnFlipperRightReleased?.Invoke();

    }
}