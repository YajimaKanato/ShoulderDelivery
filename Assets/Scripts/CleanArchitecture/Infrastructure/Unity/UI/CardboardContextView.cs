using ShoulderDelivery.Adapter;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CardboardContextView : ShowContextBase, ICardboardContextView
    {
        [SerializeField] Color _acceptColor = Color.yellow;
        [SerializeField] Color _rejectColor = Color.red;

        public void ShowCardboardAccepted(CardboardContextViewModel context)
        {
            OnInvoke(context.Context, _acceptColor);
        }

        public void ShowCardboardRejected(CardboardContextViewModel context)
        {
            OnInvoke(context.Context, _rejectColor);
        }
    }
}
