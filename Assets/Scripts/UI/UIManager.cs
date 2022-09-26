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

        private void Start()
        {
            pausePanel.SetActive(false);
            menuButton.onClick.AddListener(PauseGame);
            resumeButton.onClick.AddListener(ResumeGame);
            exitButton.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        
        
    }
}