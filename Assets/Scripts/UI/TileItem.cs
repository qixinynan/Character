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
    public class TileItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
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

        public Action OnDragEnd;
        // 拖动相关

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
            _moveTween = GetComponent<RectTransform>().DOAnchorPos(new Vector2(posX, 0), moveDuration).SetEase(Ease.OutQuad);
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
        
        #region Pointer
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isSelected && !_isDragging)
                PlayMoveAnimation(_originPosition + new Vector2(0, moveHeight));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isSelected && !_isDragging)
                PlayMoveAnimation(_originPosition);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isDragging)
                OnClicked.Invoke();
            // EventManager.OnTilePlayed.Invoke(this._data);
        }
        #endregion
        #region Drag
        private Vector2 _dragPointerOffset; // 鼠标相对于TileItem的偏移
        private Vector3 _startPosition;
        private Transform _originalParent;
        private bool _isDragging;
        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = transform.localPosition;
            _originalParent = transform.parent;
            transform.SetParent(UIManager.Instance.canvas.transform); // 提升到最顶层，避免被遮挡
            _startPosition = transform.localPosition;
            
            // 计算鼠标相对TileItem的偏移
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out _dragPointerOffset
            );
            _isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                UIManager.Instance.canvas.transform as RectTransform, 
                eventData.position, 
                eventData.pressEventCamera, 
                out var localPoint
            );
            transform.localPosition = localPoint - (Vector2)_dragPointerOffset;
            _isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(_originalParent);
            _isDragging = false;
            OnDragEnd?.Invoke();
        }
        #endregion
        
    }
}
