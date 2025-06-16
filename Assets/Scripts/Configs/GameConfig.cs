using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Config/Game Config", order = 100)]

    public class GameConfig : ScriptableObject
    {
        public string characterDataPath = "Resources/Data/character-data.csv";
    }
}
