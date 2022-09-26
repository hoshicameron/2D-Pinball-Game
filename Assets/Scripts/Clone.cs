using UnityEngine;

namespace Pinball
{
    public class Clone : MonoBehaviour
    {
        [SerializeField] private float rotationDegree = 20f;
        [SerializeField] private GameObject ballPrefab;

        public Vector2 velocity;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.CompareTag("Ball")) return;
            velocity = col.GetComponent<Rigidbody2D>().velocity;
            col.GetComponent<BallController>().ChangeBallDirection(velocity, -rotationDegree);

            var clone = Instantiate(ballPrefab, col.transform.position, Quaternion.identity);
            clone.GetComponent<BallController>().ChangeBallDirection(velocity,rotationDegree);

            gameObject.SetActive(false);
        }

       
    }
}