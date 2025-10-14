using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Config/Game Config", order = 100)]

    public class GameConfig : ScriptableObject
    {
        public string characterDataPath = "Data/character-data";
        public int characterIndexInCsv = 1;
        public int componentIndexInCsv = 2;
    }
}
