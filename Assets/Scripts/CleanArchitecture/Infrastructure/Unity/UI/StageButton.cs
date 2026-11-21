using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShoulderDelivery.Infrastructure
{
    public class StageButton : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] Image _button;
        [SerializeField] Sprite _selectButton;
        [SerializeField] Sprite _unselectButton;
        [SerializeField] Sprite _clearedButton;
        [SerializeField] float _duration = 0.5f;
        [SerializeField] Image _selectImage;
        [SerializeField] Ease _ease = Ease.OutBack;

        bool _isCleared;
        bool _isSelected;

        public void SetCleared(bool isCleared)
        {
            _isCleared = isCleared;
            UpdateSprite();
        }

        public void OpenStageSelect()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(0, 0));
            sequence.Append(transform.DOScale(1, _duration).SetEase(_ease));
        }

        void ChangeSelection(bool selected)
        {
            _isSelected = selected;
            _selectImage.enabled = selected;
            UpdateSprite();
        }

        void UpdateSprite()
        {
            if (!_button) return;
            if (_isSelected)
            {
                _button.sprite = _selectButton;
                return;
            }

            _button.sprite = _isCleared && _clearedButton ? _clearedButton : _unselectButton;
        }

        public void OnSelect(BaseEventData eventData)
        {
            ChangeSelection(true);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            ChangeSelection(false);
        }
    }
}
