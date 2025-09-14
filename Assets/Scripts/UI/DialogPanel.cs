using System;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialogPanel : MonoBehaviour
    {
        public Text dialogContentText;
        public Button confirmButton;
        
        private void Start()
        {
            confirmButton.onClick.AddListener(CloseDialog); 
        }

        public void ShowDialog(string msg)
        {
            gameObject.SetActive(true);
            dialogContentText.text = msg;
        }
        private void CloseDialog()
        {
            gameObject.SetActive(false);
        }
    }
}
