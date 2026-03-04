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
                SceneManager.LoadScene("StartUI");
            }); 
            
            replayButton.onClick.AddListener(() =>
            {
                EventManager.ClearEvents();
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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