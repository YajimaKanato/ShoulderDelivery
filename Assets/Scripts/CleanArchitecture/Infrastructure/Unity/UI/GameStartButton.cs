using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShoulderDelivery.Infrastructure
{
    public class GameStartButton : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] Image _button;
        [SerializeField] Sprite _selectButton;
        [SerializeField] Sprite _unselectButton;
        [SerializeField] Image _selectImage;

        void ChangeSelection(bool selected)
        {
            if (_selectImage != null) _selectImage.enabled = selected;

            var sprite = selected ? _selectButton : _unselectButton;
            if (_button != null && sprite != null) _button.sprite = sprite;
        }

        private void OnDisable() => ChangeSelection(false);

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
