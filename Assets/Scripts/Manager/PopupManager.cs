using System;
using Game.Player;
using UI;
using UnityEngine;

namespace Manager
{
    public class PopupManager: MonoBehaviour
    {

        public GameOverPanel gameOverPanel;
        public PausePanel pausePanel;
        private void Start()
        {
            EventManager.OnGameOver += ShowGameOverPanel;
        }

        public void ShowGameOverPanel(BasePlayer player)
        {
            gameOverPanel.Show();
        }

        public void ShowPausePanel()
        {
            pausePanel.Show();
        }
    }
}