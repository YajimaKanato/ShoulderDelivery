using ShoulderDelivery.Adapter;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class DeliveryView : ShowContextBase, IDeliveriedView
    {
        [SerializeField] Color _successColor = Color.yellow;
        [SerializeField] Color _failedColor = Color.red;

        public void ShowDeliveryFailedResult(DeliveriedViewModel context)
        {
            OnInvoke("", _failedColor);
        }

        public void ShowDeliverySucceededResult(DeliveriedViewModel context)
        {
            OnInvoke("", _successColor);
        }
    }
}
