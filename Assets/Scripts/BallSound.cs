using UnityEngine;

namespace Pinball
{
    public class BallSound : MonoBehaviour
    {
        private AudioSource sound;
        private bool soundFlag = false;
        void Awake()
        {
            TryGetComponent<AudioSource>(out sound);
        }

        void OnCollisionEnter2D(Collision2D evt)
        {
            if (sound != null && soundFlag == false)
            {
                soundFlag = true;
                sound.Play();
            }
        }

        void OnCollisionExit2D(Collision2D evt)
        {
            soundFlag = false;
        }
    
    }
}
