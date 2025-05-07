using System;
using System.Collections.Generic;
using Game.GameRule;
using Manager;
using UnityEngine;

namespace Game.Player
{
    public abstract class BasePlayer
    {
        public readonly int Id;
        public List<TileData> HandTiles = new List<TileData>();
        private readonly IGameRule _gameRule;
        public Action OnRoundEnd;

        protected BasePlayer(int id, IGameRule gameRule)
        {
            Id = id;
            _gameRule = gameRule;
        }

        public abstract void StartRound();

        private void AddTile(TileData tile)
        {
            HandTiles.Add(tile);
        }

        public void InitTiles()
        {
            HandTiles = _gameRule.GenerateTiles(_gameRule.GetFirstGenerateTileCount());
            UpdateTileUI(TileChangeInfo.InitInfo(HandTiles));
        }

        public void PlayTiles(List<TileData> tileList)
        {
            foreach (var tile in tileList)
            {
                UpdateTileUI(TileChangeInfo.PlayInfo(HandTiles, tile));
                HandTiles.Remove(tile);
            }
            OnRoundEnd.Invoke();
        }
        
        void UpdateTileUI(TileChangeInfo i)
        {
            EventManager.OnTilesChanged.Invoke(i);
        } 
        protected virtual TileData DrawTile()
        {
           // varr newTile = IGameRule. 
           var newTile = _gameRule.GenerateTiles(1)[0];
           AddTile(newTile);
           UpdateTileUI(TileChangeInfo.DrawInfo(HandTiles, newTile));
           Debug.Log("摸牌,目前牌数:" + HandTiles.Count);
           return newTile;
        } 
    }
}