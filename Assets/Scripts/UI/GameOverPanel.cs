using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOverPanel : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            SceneManager.LoadScene("StartUI");
        }
    }
}