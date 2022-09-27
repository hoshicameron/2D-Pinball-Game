using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Pinball.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel= null;
        [SerializeField] private Button menuButton = null;
        [SerializeField] private Button resumeButton = null;
        [SerializeField] private Button exitButton = null;

        private bool pause = false;
        private void Start()
        {
            pausePanel.SetActive(false);
            menuButton.onClick.AddListener(PauseMenu);
            resumeButton.onClick.AddListener(PauseMenu);
            exitButton.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        public void PauseMenu()
        {
            pause = !pause;
            Time.timeScale = pause ? 0 : 1;
            pausePanel.SetActive(pause);
        }

    }
}