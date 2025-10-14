using System;
using DG.Tweening;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PausePanel: MonoBehaviour
    {
        public Button replayButton;
        public Button exitGameButton;

        private void OnEnable()
        {
            exitGameButton.onClick.AddListener(() =>
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif

            }); 
            
            replayButton.onClick.AddListener(() =>
            {
                EventManager.ClearEvents();
                SceneManager.LoadScene("Game");
            });
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}