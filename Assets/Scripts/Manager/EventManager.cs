using System;
using System.Collections.Generic;
using Game;
using Game.Player;
using UI;
using UnityEngine.Events;

namespace Manager
{
    public static class EventManager
    {
        // TODO: 修改成内部触发
        // public static UnityAction<TileData> OnTilePlayed;
        public static Action<List<TileData>> OnTilesPlayed; // 当牌被打出，不一定成功打出
        public static Action<string> OnPlayerCharacterComposed; // 当某个字被成功组合打出
        public static Action<string> OnAICharacterComposed;
        public static Action<TileChangeInfo> OnTilesChanged;
        public static Action<List<TileData>, int> OnAITilesPlayed; // AI 打出的牌 + 剩余手牌数
        public static Action<string> OnAILogged; // AI 决策过程日志
        public static Action OnAnyRoundStart;
        public static Action OnAnyRoundEnd;
        public static Action<BasePlayer> OnGameOver;

        public static void ClearEvents()
        {
            OnTilesPlayed = null;
            OnPlayerCharacterComposed = null;
            OnTilesChanged = null;
            OnAITilesPlayed = null;
            OnAILogged = null;
            OnAnyRoundStart = null;
            OnAnyRoundEnd = null;
            OnGameOver = null;
        }
    }
}
