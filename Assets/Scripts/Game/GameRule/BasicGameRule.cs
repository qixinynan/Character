using System.Collections.Generic;
using System.Linq;
using Game.Player;
using JetBrains.Annotations;
using UnityEngine;
using Util;

namespace Game.GameRule
{
    public class BasicGameRule : IGameRule
    {
        private CharacterResources _characterResources;
        private readonly List<TileData> _tileCardPoolList = new List<TileData>();
        private readonly int _cardPoolCount = 5; // TODO: to config

        public void Init(CharacterResources characterResources)
        {
            this._characterResources = characterResources;
        }

        private void FullTileCardPool()
        {
            var frequencies = _characterResources.GetComponents();
            _tileCardPoolList.Clear();
            // 构建权重池
            List<string> weightedPool = new();
            foreach (var pair in frequencies)
            {
                for (int i = 0; i < pair.Value; i++) // 出现一次就加一次
                {
                    weightedPool.Add(pair.Key);
                }
            }

            for (int i = 0; i < _cardPoolCount; i++)
            {
                if (weightedPool.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, weightedPool.Count);
                    string component = weightedPool[randomIndex];
                    _tileCardPoolList.Add(new TileData(i, component));
                }
            }
        }
            
        public List<TileData> GenerateTiles(int count)
        { 
            List<TileData> tileData = new List<TileData>();
            for (int i = 0; i < count; i++)
            {
                if (_tileCardPoolList.Count <= 0)
                    FullTileCardPool();
                tileData.Add(_tileCardPoolList[^1]);
                _tileCardPoolList.RemoveAt(_tileCardPoolList.Count -1); 
            }
            
            return tileData;
        }


        public int GetFirstGenerateTileCount()
        {
            return 13; // TODO: maybe in config and move config instance to BasicGameRule class
        }

        public Result<string> IsTilesPlayable(List<TileData> tiles)
        {
            if (tiles == null || tiles.Count == 0)
                return Result<string>.Error("未选择任何牌");
            
            if (tiles.Count == 1)
            {
                return Result<string>.OkResult(""); // 过牌
            }

            Debug.Log("开始检测组合字, 目前已存在的字数量: " + _characterResources.GetAllCharacters().Count());
            var selectedComponents = tiles.Select(t => t.Content).ToHashSet();
            foreach (var character in _characterResources.GetAllCharacters())
            {
                var components = character.Components.ToHashSet();
                if (selectedComponents.SetEquals(components))
                {
                    Debug.Log("可以组成汉字");
                    return Result<string>.OkResult(character.Character);
                }
            }

            return Result<string>.Error("没有此组合");
        }

        public bool CheckWin(BasePlayer player)
        {
            if (player.HandTiles.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}