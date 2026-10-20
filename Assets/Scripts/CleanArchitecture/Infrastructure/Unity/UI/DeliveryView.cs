using ShoulderDelivery.Adapter;
using UnityEngine;
using System;

namespace ShoulderDelivery.Infrastructure
{
    public class DeliveryView : MonoBehaviour, IDeliveriedView
    {
        [SerializeField] Color _successColor = Color.yellow;
        [SerializeField] Color _failedColor = Color.red;

        LogManager _logManager;

        public void Init(LogManager logManager)
        {
            _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
        }

        public void ShowDeliveryFailedResult(DeliveriedViewModel context)
        {
            _logManager.ShowContext("", _failedColor);
        }

        public void ShowDeliverySucceededResult(DeliveriedViewModel context)
        {
            _logManager.ShowContext("", _successColor);
        }
    }
}
