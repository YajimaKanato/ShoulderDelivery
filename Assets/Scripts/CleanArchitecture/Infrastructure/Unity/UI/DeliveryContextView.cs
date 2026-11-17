using ShoulderDelivery.Adapter;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class DeliveryContextView : ShowContextBase, IDeliveriedView, IScorePenaltyView
    {
        [SerializeField] Color _successColor = Color.yellow;
        [SerializeField] Color _failedColor = Color.red;
        [SerializeField] Sprite _successSprite;
        [SerializeField] Sprite _failedSprite;

        public void ShowCitizenHitPenalty(int deductedScore)
        {
            OnInvoke($"Citizen hit -{deductedScore}", _failedColor, _failedSprite);
        }

        public void ShowDeliveryFailedResult(DeliveriedViewModel context)
        {
            OnInvoke("Delivery missed", _failedColor, _failedSprite);
        }

        public void ShowDeliverySucceededResult(DeliveriedViewModel context)
        {
            OnInvoke($"Delivered +{context.CurrentGetScore}", _successColor, _successSprite);
        }
    }
}
