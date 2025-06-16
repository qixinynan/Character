using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TilePanel : MonoBehaviour
    {
        public TileItem tilePrefab;
        public float newDrawTileGap = 40f;

        private readonly List<TileItem> _tileItems = new List<TileItem>();

        private void Start()
        {
            EventManager.OnTilesChanged += UpdateTiles;
            EventManager.OnAnyRoundEnd += (() =>
            {
                _tileItems.ForEach(item => item.isNewDraw = false);
            });
        }

        private void PlaceTilePosition(TileItem item)
        {
            
            int index = _tileItems.IndexOf(item);
            var rectTransform = item.GetComponent<RectTransform>();
            if (item.isNewDraw)
            {
                item.MoveXTo(rectTransform.sizeDelta.x * index + newDrawTileGap);   
            }
            else
            {
                item.MoveXTo(rectTransform.sizeDelta.x * index);
            }
        }

        private TileItem NewTile(TileData data)
        {
            TileItem item = Instantiate(tilePrefab, transform);
            item.Init(data);
            item.OnClicked += () =>
            {
                // 动画和其他逻辑
                item.ToggleSelect();
            };
            return item;
        }
        
        private void UpdateTiles(TileChangeInfo info)
        {
            // 记录已有的牌的数据，用来比较
            List<TileData> oldTileDatas = new List<TileData>(_tileItems.Select(item => item.GetData()));
            // 打出牌
            foreach (var item in _tileItems.ToList())
            {
                if (!info.TileList.Contains(item.GetData()))
                {
                    _tileItems.Remove(item);
                    DOTween.Kill(item.GetComponent<RectTransform>());
                    Destroy(item.gameObject);
                }
            }

            // 新增牌
            foreach (var data in info.TileList)
            {
                // 如果是新增的牌
                if (!oldTileDatas.Contains(data))
                {
                    var item = NewTile(data);
                    
                    // 标记新牌
                    if (info.Type == TileOperationType.Draw && info.ModifiedTile.Id == data.Id)
                    {
                        item.isNewDraw = true;
                    }
                    _tileItems.Add(item);
                }
            }

            foreach (var item in _tileItems)
            {
                PlaceTilePosition(item);    
            }
        }

        
        public List<TileItem> GetSelectedTiles()
        {
            List<TileItem> selectedTiles = new List<TileItem>();
            foreach (var tile in _tileItems)
            {
                if (tile.IsSelected())
                {
                    selectedTiles.Add(tile);
                }
            }
            return selectedTiles;
        }
    }
}
