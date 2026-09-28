using ShoulderDelivery.Adapter;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CardboardContextView : MonoBehaviour, ICardboardContextView
    {
        [SerializeField] CardboardContextText[] _cardboardContextTexts;
        [SerializeField] float[] _coordinatesY;
        [SerializeField] int _viewCount = 5;

        bool IsAssigned => _cardboardContextTexts != null && _cardboardContextTexts.Length > 0;

        public void ShowCardboardAccepted(CardboardContextViewModel context)
        {
            if (!IsAssigned) return;
        }

        public void ShowCardboardRejected(CardboardContextViewModel context)
        {

        }

        private void OnValidate()
        {
            var count = _viewCount * 2 + 1;
            if (_cardboardContextTexts == null || _cardboardContextTexts.Length != count)
                _cardboardContextTexts = new CardboardContextText[count];

            if (_coordinatesY == null || _coordinatesY.Length != count)
                _coordinatesY = new float[count];
        }
    }
}
