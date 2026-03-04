using System;
using System.Collections.Generic;
using Game;
using Manager;
using UnityEngine;

namespace UI
{
    public class CharacterViewPanel: MonoBehaviour
    {
        public CharacterView characterViewPrefab;

        private void OnEnable()
        {
            EventManager.OnPlayerCharacterComposed += OnCharacterComposed;
            EventManager.OnAICharacterComposed += OnAIComposed;
        }

        private void OnDisable()
        {
            EventManager.OnPlayerCharacterComposed -= OnCharacterComposed;
            EventManager.OnAICharacterComposed -= OnAIComposed;
        }

        private void OnCharacterComposed(string obj)
        {
            AddCharacter(obj, Color.white, Color.black);   
        }

        private void OnAIComposed(string obj)
        {
            AddCharacter(obj, Color.red, Color.white);
        }
        

        public void AddCharacter(string character, Color color, Color textColor)
        {
            var obj = Instantiate(characterViewPrefab, transform);
            obj.SetText(character);
            obj.SetColor(color);
            obj.SetTextColor(textColor);
        }
    }
}