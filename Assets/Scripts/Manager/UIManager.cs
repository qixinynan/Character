using System;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        public TilePanel tilePanel;
        public DialogPanel dialogPanel;
        public PopupManager popupManager;
        
        public Button playButton;
        public static UIManager Instance;
        [HideInInspector] public Canvas canvas;

        private void Awake()
        {
            Instance = this;
            canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            playButton.onClick.AddListener(PlayHandler);
        }

        private void PlayHandler()
        {
            // TODO: Filter out the case where no tiles are selected
            EventManager.OnTilesPlayed.Invoke(tilePanel.GetSelectedTiles().Select(e => e.GetData()).ToList());
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                popupManager.ShowPausePanel();
            }
        }

        public void ShowDialog(string msg)
        {
            dialogPanel.ShowDialog(msg); 
        }
    }
}
