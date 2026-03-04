using System.Collections.Generic;
using System.Linq;
using Game.Player;
using Manager;
using Util;

namespace Game.GameRule
{
    // 人机模式规则: 规则判定与基础模式一致, 额外提供 AI 选牌逻辑
    public class VsAIGameRule : IGameRule
    {
        public class AIDecision
        {
            public readonly List<TileData> SelectedTiles;
            public readonly List<string> Logs;

            public AIDecision(List<TileData> selectedTiles, List<string> logs)
            {
                SelectedTiles = selectedTiles;
                Logs = logs;
            }
        }

        private readonly BasicGameRule _basicRule = new BasicGameRule();

        public void Init(CharacterResources res)
        {
            _basicRule.Init(res);
        }

        public List<TileData> GenerateTiles(int count)
        {
            return _basicRule.GenerateTiles(count);
        }

        public int GetFirstGenerateTileCount()
        {
            return _basicRule.GetFirstGenerateTileCount();
        }

        public Result<string> IsTilesPlayable(List<TileData> tiles)
        {
            return _basicRule.IsTilesPlayable(tiles);
        }

        public bool CheckWin(BasePlayer player)
        {
            return _basicRule.CheckWin(player);
        }

        public void OnGameOver(BasePlayer winner)
        {
            BattleRecordManager.Instance.AddRecord(winner.IsHumanPlayer);
        }

        public List<TileData> DecideAiPlayTiles(List<TileData> handTiles)
        {
            return DecideAiPlay(handTiles).SelectedTiles;
        }

        public AIDecision DecideAiPlay(List<TileData> handTiles)
        {
            var logs = new List<string>();
            if (handTiles == null || handTiles.Count == 0)
            {
                logs.Add("手牌为空，无法出牌。");
                return new AIDecision(new List<TileData>(), logs);
            }

            logs.Add("当前手牌: " + string.Join(",", handTiles.Select(t => t.Content)));
            // 优先打出可组成汉字的组合, 组合越长优先级越高
            int maxSize = handTiles.Count < 4 ? handTiles.Count : 4;
            int triedCount = 0;
            for (int size = maxSize; size >= 2; size--)
            {
                logs.Add("尝试组合长度: " + size);
                foreach (var combo in GetCombinations(handTiles, size))
                {
                    triedCount++;
                    string comboText = string.Join("", combo.Select(t => t.Content));
                    var checkResult = IsTilesPlayable(combo);
                    if (checkResult.IsOk && !string.IsNullOrEmpty(checkResult.Data))
                    {
                        logs.Add("命中可组字组合: " + comboText + " -> " + checkResult.Data);
                        logs.Add("本回合共尝试组合数: " + triedCount);
                        return new AIDecision(combo, logs);
                    }

                    logs.Add("组合不可出: " + comboText);
                }
            }

            // 没有可组字时, 默认打一张最左侧手牌
            var fallback = new List<TileData> { handTiles[0] };
            logs.Add("无可组字组合，默认打一张: " + handTiles[0].Content);
            logs.Add("本回合共尝试组合数: " + triedCount);
            return new AIDecision(fallback, logs);
        }

        private IEnumerable<List<TileData>> GetCombinations(List<TileData> source, int size)
        {
            int n = source.Count;
            int[] indices = new int[size];
            for (int i = 0; i < size; i++)
            {
                indices[i] = i;
            }

            while (true)
            {
                var result = new List<TileData>(size);
                for (int i = 0; i < size; i++)
                {
                    result.Add(source[indices[i]]);
                }
                yield return result;

                int t = size - 1;
                while (t >= 0 && indices[t] == n - size + t)
                {
                    t--;
                }

                if (t < 0)
                {
                    yield break;
                }

                indices[t]++;
                for (int i = t + 1; i < size; i++)
                {
                    indices[i] = indices[i - 1] + 1;
                }
            }
        }
    }
}
