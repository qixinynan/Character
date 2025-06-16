using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Game.GameRule
{
    public class BasicGameRule : IGameRule
    {
        private CharacterResources _characterResources;

        public void Init(CharacterResources characterResources)
        {
            this._characterResources = characterResources;
        }

        public List<TileData> GenerateTiles(int count)
        {
            var frequencies = _characterResources.GetComponents();
            var tileDatas = new List<TileData>();

            // 构建权重池
            List<string> weightedPool = new();
            foreach (var pair in frequencies)
            {
                for (int i = 0; i < pair.Value; i++) // 出现一次就加一次
                {
                    weightedPool.Add(pair.Key);
                }
            }

            for (int i = 0; i < count; i++)
            {
                if (weightedPool.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, weightedPool.Count);
                    string component = weightedPool[randomIndex];
                    tileDatas.Add(new TileData(i, component));
                }
            }

            return tileDatas;
        }


        public int GetFirstGenerateTileCount()
        {
            return 13;
        }

        public Result IsTilesPlayable(List<TileData> tiles)
        {
            if (tiles == null || tiles.Count == 0)
                return new Result(ResultType.Error, "未选择任何牌");
            
            if (tiles.Count == 1)
            {
                return Result.Ok;
            }

            Debug.Log("开始检测组合字, 目前已存在的字数量: " + _characterResources.GetAllCharacters().Count());
            var selectedComponents = tiles.Select(t => t.Content).ToHashSet();
            foreach (var character in _characterResources.GetAllCharacters())
            {
                var components = character.Components.ToHashSet();
                if (selectedComponents.SetEquals(components))
                {
                    Debug.Log("可以组成汉字");
                    return Result.Ok;
                }
            }

            return new Result(ResultType.Error, "数量很多目前没有实现"); // TODO
        }
    }
}