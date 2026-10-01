using ShoulderDelivery.Adapter;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CardboardContextView : MonoBehaviour, ICardboardContextView
    {
        [SerializeField] ContextText[] _cardboardContextTexts;
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
    }
}
