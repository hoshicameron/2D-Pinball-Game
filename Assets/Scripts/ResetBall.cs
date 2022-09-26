using System;
using UnityEngine;

namespace Pinball
{
    public class ResetBall : MonoBehaviour
    {
        private Vector3 ballPos;

        private void Start()
        {
            ballPos = GameObject.FindGameObjectWithTag("Ball").transform.position;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Ball"))
            {
                col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                col.transform.position = ballPos;

            }
        }
    }
}