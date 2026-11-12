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
