using System;
using DG.Tweening;
using Game;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class TileItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public float moveHeight = 30f;
        public float selectedHeight = 40f;
        public float moveDuration = 0.2f;
        public RectTransform tileContent;
        public Text text;
        public UnityAction OnClicked;
        public bool isNewDraw; // 是否是新发的牌
        
        private TileData _data;
        private Vector2 _originPosition;
        private Tween _hoverTween;
        private Tween _moveTween;
        private bool _isSelected = false;

        private void Awake()
        {
            _originPosition = tileContent.anchoredPosition;
        }

        public void Init(TileData data)
        {
            _data = data;
            text.text = _data.Content;
        }

        public TileData GetData()
        {
            return _data;
        }

        private void PlayMoveAnimation(Vector2 targetPos)
        {
            if (_hoverTween != null && _hoverTween.IsActive()) {
                _hoverTween.Kill();
            }
            _hoverTween = tileContent.DOAnchorPos(targetPos, moveDuration).SetEase(Ease.OutQuad);
        }

        public void MoveXTo(float posX)
        {
            _moveTween = GetComponent<RectTransform>().DOAnchorPosX(posX, moveDuration).SetEase(Ease.OutQuad);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isSelected)
                PlayMoveAnimation(_originPosition + new Vector2(0, moveHeight));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isSelected)
                PlayMoveAnimation(_originPosition);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked.Invoke();
            // EventManager.OnTilePlayed.Invoke(this._data);
        }

        public void ToggleSelect()
        {
            _isSelected = !_isSelected;
            if (_isSelected)
                PlayMoveAnimation(_originPosition + new Vector2(0, selectedHeight));
            else 
                PlayMoveAnimation(_originPosition);
        }

        public bool IsSelected()
        {
            return _isSelected;
        }

        private void OnDestroy()
        {
            _hoverTween.Kill();
            _moveTween.Kill();
        }
    }
}
