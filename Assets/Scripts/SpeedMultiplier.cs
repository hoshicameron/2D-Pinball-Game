using System;

using UnityEngine;

namespace Pinball
{
    public class SpeedMultiplier : MonoBehaviour
    {
        [SerializeField] private float multiplierValue;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.CompareTag("Ball")) return;
            
            col.GetComponent<BallController>().BallSpeedMultiplier(multiplierValue);
        }
    }
}