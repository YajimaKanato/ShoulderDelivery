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
        [SerializeField] float _duration = 0.5f;
        [SerializeField] Image _selectImage;
        [SerializeField] Ease _ease = Ease.OutBack;

        public void OpenStageSelect()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(0, 0));
            sequence.Append(transform.DOScale(1, _duration).SetEase(_ease));
        }

        void ChangeSelection(bool selected)
        {
            _selectImage.enabled = selected;

            if (selected)
            {
                _button.sprite = _selectButton;
            }
            else
            {
                _button.sprite = _unselectButton;
            }
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
