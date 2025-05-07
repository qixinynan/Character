using System.Collections.Generic;

namespace Game
{
    public enum TileOperationType
    {
        Init,           // 开局摸牌
        Draw,           // 摸牌
        Play,           // 出牌
        Refresh         // 完整刷新（比如起始发牌）
    }

    public struct TileChangeInfo
    {
        // TODO: 加入 Player 区分操作
        public TileOperationType Type;
        public List<TileData> TileList; // 最新的牌列表
        public TileData ModifiedTile; // 哪张牌被操作了（可选）

        public TileChangeInfo(TileOperationType op, List<TileData> tileList, TileData modifiedTile = null)
        {
            Type = op;
            TileList = tileList;
            ModifiedTile = modifiedTile;
        }

        public static TileChangeInfo InitInfo(List<TileData> tileList)
        {
            return new TileChangeInfo(TileOperationType.Init, tileList);
        }

        public static TileChangeInfo DrawInfo(List<TileData> tileList, TileData newTile)
        {
            return new TileChangeInfo(TileOperationType.Draw, tileList, newTile);
        }
        
        public static TileChangeInfo PlayInfo(List<TileData> tileList, TileData playedData)
        {
            return new TileChangeInfo(TileOperationType.Play, tileList, playedData);
        }
    }
}