using System.Collections.Generic;
using Util;

namespace Game.GameRule
{
    public interface IGameRule
    {
        public void Init(CharacterResources res);
        public List<TileData> GenerateTiles(int count);

        public int GetFirstGenerateTileCount();
        public Result IsTilesPlayable(List<TileData> tiles);
    }
}
