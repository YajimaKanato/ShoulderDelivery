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
            var message = $"配達成功！ +{context.CurrentGetScore}";
            if (context.SpeedBonus > 0 || context.DistanceBonus > 0)
            {
                var speed = context.SpeedBonus > 0 ? $"速度 +{context.SpeedBonus}" : "";
                var distance = context.DistanceBonus > 0 ? $"距離 +{context.DistanceBonus}" : "";
                var separator = context.SpeedBonus > 0 && context.DistanceBonus > 0 ? "  " : "";
                message += $"\n{speed}{separator}{distance}";
            }
            OnInvoke(message, _successColor, _successSprite);
        }
    }
}
