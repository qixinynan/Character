using System.Collections.Generic;

namespace Game
{
    public class CharacterResources
    {
        private readonly List<CharacterData> _characterDataList = new List<CharacterData>();
        private Dictionary<string, int> _componentFrequency = new();

        public void AddCharacter(CharacterData characterData)
        {
            _characterDataList.Add(characterData);
            
        }

        public void AddComponent(string component) {
            if (!_componentFrequency.TryAdd(component, 1))
                _componentFrequency[component]++;
        }

        public List<CharacterData> GetAllCharacters()
        {
            return _characterDataList;
        }

        public Dictionary<string, int> GetComponents()
        {
            return _componentFrequency;
        }

        public int GetComponentCount()
        {
            return _componentFrequency.Count;
        }

    }
}