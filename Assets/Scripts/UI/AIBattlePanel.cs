using System.Collections.Generic;
using System.Linq;
using Game;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    // 场景内 AI 信息面板: 显示 AI 出牌、剩余手牌数、决策日志
    public class AIBattlePanel : MonoBehaviour
    {
        public Text playedTilesText;
        public Text remainCountText;
        public Text logsText;

        private const int MaxLogLines = 12;
        private readonly Queue<string> _logQueue = new Queue<string>();

        private void OnEnable()
        {
            EventManager.OnAITilesPlayed += HandleAITilesPlayed;
            EventManager.OnAILogged += HandleAILogged;
        }

        private void OnDisable()
        {
            EventManager.OnAITilesPlayed -= HandleAITilesPlayed;
            EventManager.OnAILogged -= HandleAILogged;
        }

        private void HandleAITilesPlayed(List<TileData> playedTiles, int remainCount)
        {
            if (playedTilesText == null || remainCountText == null)
            {
                return;
            }

            string played = (playedTiles == null || playedTiles.Count == 0)
                ? "-"
                : string.Join(",", playedTiles.Select(t => t.Content));

            playedTilesText.text = "AI出牌: " + played;
            remainCountText.text = "AI剩余手牌: " + remainCount;
        }

        private void HandleAILogged(string log)
        {
            if (logsText == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(log))
            {
                return;
            }

            _logQueue.Enqueue(log);
            while (_logQueue.Count > MaxLogLines)
            {
                _logQueue.Dequeue();
            }

            logsText.text = "AI日志:\n" + string.Join("\n", _logQueue);
        }
    }
}
