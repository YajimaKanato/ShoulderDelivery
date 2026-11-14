using ShoulderDelivery.Adapter;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class DeliveryContextView : ShowContextBase, IDeliveriedView, IScorePenaltyView
    {
        [SerializeField] Color _successColor = Color.yellow;
        [SerializeField] Color _failedColor = Color.red;

        public void ShowCitizenHitPenalty(int deductedScore)
        {
            OnInvoke($"Citizen hit -{deductedScore}", _failedColor);
        }

        public void ShowDeliveryFailedResult(DeliveriedViewModel context)
        {
            OnInvoke("Delivery missed", _failedColor);
        }

        public void ShowDeliverySucceededResult(DeliveriedViewModel context)
        {
            OnInvoke($"Delivered +{context.CurrentGetScore}", _successColor);
        }
    }
}
