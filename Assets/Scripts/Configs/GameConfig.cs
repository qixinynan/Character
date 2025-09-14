using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Config/Game Config", order = 100)]

    public class GameConfig : ScriptableObject
    {
        public string characterDataPath = "Data/character-data";
    }
}
