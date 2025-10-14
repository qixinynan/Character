using System.Collections.Generic;
using Game.Player;
using Util;

namespace Game.GameRule
{
    public interface IGameRule
    {
        public void Init(CharacterResources res);
        public List<TileData> GenerateTiles(int count);

        public int GetFirstGenerateTileCount();
        public Result<string> IsTilesPlayable(List<TileData> tiles);
        public bool CheckWin(BasePlayer player);
    }
}
