using System.Collections.Generic;
using Game.GameRule;
using Manager;
using UnityEngine;

namespace Game.Player
{
    public class AIPlayer : BasePlayer
    {
        private readonly VsAIGameRule _vsAiRule;

        public AIPlayer(int id, IGameRule gameRule) : base(id, gameRule)
        {
            _vsAiRule = gameRule as VsAIGameRule;
        }

        protected override bool EnableTileUISync => false;

        public override void StartRound()
        {
            base.StartRound();
            DrawTile();
        }

        public void PlayAutoTurn()
        {
            EmitLog("AI回合开始，当前手牌数: " + HandTiles.Count);
            var decision = DecidePlayDecision();
            List<TileData> playTiles = decision.SelectedTiles;
            foreach (var log in decision.Logs)
            {
                EmitLog(log);
            }

            if (playTiles.Count == 0)
            {
                EmitLog("AI无可打出的牌，跳过本回合。");
                EventManager.OnAITilesPlayed?.Invoke(new List<TileData>(), HandTiles.Count);
                PlayTiles(new List<TileData>());
                return;
            }

            EmitLog("AI选择打出: " + string.Join(",", playTiles.ConvertAll(t => t.Content)));
            var playResult = GameRule.IsTilesPlayable(playTiles);
            if (playResult.IsOk && !string.IsNullOrEmpty(playResult.Data))
            {
                EmitLog("AI成功组字: " + playResult.Data);
                EventManager.OnCharacterComposed?.Invoke(playResult.Data);
            }

            PlayTiles(playTiles);
            EventManager.OnAITilesPlayed?.Invoke(new List<TileData>(playTiles), HandTiles.Count);
            EmitLog("AI回合结束，剩余手牌数: " + HandTiles.Count);
        }

        private VsAIGameRule.AIDecision DecidePlayDecision()
        {
            if (_vsAiRule != null)
            {
                return _vsAiRule.DecideAiPlay(HandTiles);
            }

            if (HandTiles.Count > 0)
            {
                return new VsAIGameRule.AIDecision(
                    new List<TileData> { HandTiles[0] },
                    new List<string> { "规则非 VsAIGameRule，降级为默认出第一张。" }
                );
            }

            return new VsAIGameRule.AIDecision(
                new List<TileData>(),
                new List<string> { "规则非 VsAIGameRule 且无手牌。" }
            );
        }

        private void EmitLog(string log)
        {
            Debug.Log("[AI] " + log);
            EventManager.OnAILogged?.Invoke(log);
        }
    }
}
