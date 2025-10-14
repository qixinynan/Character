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
            EventManager.OnCharacterComposed += OnCharacterComposed;
        }

        private void OnDisable()
        {
            EventManager.OnCharacterComposed -= OnCharacterComposed;
        }

        private void OnCharacterComposed(string obj)
        {
            AddCharacter(obj);   
        }
        

        public void AddCharacter(string character)
        {
            var obj = Instantiate(characterViewPrefab, transform);
            obj.SetText(character);
        }
    }
}