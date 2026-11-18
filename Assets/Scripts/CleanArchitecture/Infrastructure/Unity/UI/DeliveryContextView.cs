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
            OnInvoke($"市民に衝突！ -{deductedScore}", _failedColor, _failedSprite);
        }

        public void ShowDeliveryFailedResult(DeliveriedViewModel context)
        {
            OnInvoke("配達失敗！", _failedColor, _failedSprite);
        }

        public void ShowDeliverySucceededResult(DeliveriedViewModel context)
        {
            OnInvoke($"配達成功！ +{context.CurrentGetScore}", _successColor, _successSprite);
        }
    }
}
