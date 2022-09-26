using System;
using UnityEngine;

namespace Pinball
{
    public class GameFrameRate : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 30;
        }
    }
}