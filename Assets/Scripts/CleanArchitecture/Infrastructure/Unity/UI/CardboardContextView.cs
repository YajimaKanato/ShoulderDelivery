using ShoulderDelivery.Adapter;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CardboardContextView : ShowContextBase, ICardboardContextView
    {
        [SerializeField] Color _acceptColor = Color.yellow;
        [SerializeField] Color _rejectColor = Color.red;
        [SerializeField] Sprite _successSprite;
        [SerializeField] Sprite _failedSprite;

        public void ShowCardboardAccepted(CardboardContextViewModel context)
        {
            OnInvoke(context.Context, _acceptColor, _successSprite);
        }

        public void ShowCardboardRejected(CardboardContextViewModel context)
        {
            OnInvoke(context.Context, _rejectColor, _failedSprite);
        }
    }
}
