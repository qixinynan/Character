using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterView : MonoBehaviour
    {
        public TextMeshProUGUI text;

        public void SetText(string t)
        {
            text.text = t;
        }
    }
}