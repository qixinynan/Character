using System;
using System.Collections.Generic;
using Game;
using UI;
using UnityEngine.Events;

namespace Manager
{
    public static class EventManager
    {
        // TODO: 修改成内部触发
        // public static UnityAction<TileData> OnTilePlayed;
        public static UnityAction<List<TileData>> OnTilesPlayed;
        public static Action<TileChangeInfo> OnTilesChanged;
        public static Action OnAnyRoundStart;
        public static Action OnAnyRoundEnd;
    }
}