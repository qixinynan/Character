using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
        private Color _playedTilesDefaultColor;
        private bool _hasCachedDefaultColor;

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
                : string.Join("", playedTiles.Select(t => "[" + t.Content + "]"));

            playedTilesText.text = "AI出牌: " + played;
            remainCountText.text = "AI剩余手牌: " + remainCount;
            PlayPlayedTilesAnimation();
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

        private void PlayPlayedTilesAnimation()
        {
            if (playedTilesText == null)
            {
                return;
            }

            if (!_hasCachedDefaultColor)
            {
                _playedTilesDefaultColor = playedTilesText.color;
                _hasCachedDefaultColor = true;
            }

            var rectTransform = playedTilesText.rectTransform;
            DOTween.Kill(rectTransform);
            DOTween.Kill(playedTilesText);

            rectTransform.localScale = Vector3.one;
            Sequence seq = DOTween.Sequence();
            seq.Append(rectTransform.DOScale(1.12f, 0.12f));
            seq.Append(rectTransform.DOScale(1f, 0.18f));

            playedTilesText.color = _playedTilesDefaultColor;
            playedTilesText.DOColor(new Color(1f, 0.85f, 0.3f, 1f), 0.1f)
                .OnComplete(() => { playedTilesText.DOColor(_playedTilesDefaultColor, 0.22f); });
        }
    }
}
