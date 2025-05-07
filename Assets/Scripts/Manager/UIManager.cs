using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        public TilePanel tilePanel;
        public Button playButton;

        private void Start()
        {
            playButton.onClick.AddListener(PlayHandler);
        }

        private void PlayHandler()
        {
            // TODO: Filter out the case where no tiles are selected
            EventManager.OnTilesPlayed.Invoke(tilePanel.GetSelectedTiles().Select(e => e.GetData()).ToList());
        }
    }
}
