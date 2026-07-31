using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>スコア計算に必要な基本データを持つクラス</summary>
    public sealed class ScoreRules
    {
        readonly int _deliverySuccessScore;
        readonly DeliveryComboBonusRule _deliveryComboBonus;
        readonly SpeedBonusRule _speedBonus;
        readonly DistanceBonusRule _distanceBonus;
        readonly RemainingTimeBonusRule _remainingTimeBonus;

        /// <summary>配達成功時の基礎スコア</summary>
        public int DeliverySuccessScore => _deliverySuccessScore;
        /// <summary>連続配達成功回数に応じたボーナススコア</summary>
        public DeliveryComboBonusRule DeliveryComboBonus => _deliveryComboBonus;
        /// <summary>移動速度に応じたボーナススコア</summary>
        public SpeedBonusRule SpeedBonusBonus => _speedBonus;
        /// <summary>距離に応じたボーナススコア</summary>
        public DistanceBonusRule DistanceBonus => _distanceBonus;
        /// <summary>残り時間に応じたボーナススコア</summary>
        public RemainingTimeBonusRule RemainingTimeBonus => _remainingTimeBonus;

        public ScoreRules(int deliverySuccessScore
            , DeliveryComboBonusRule deliveryComboBonus
            , SpeedBonusRule speedBonus
            , DistanceBonusRule distanceBonus
            , RemainingTimeBonusRule remainingTimeBonus)
        {
            if (deliverySuccessScore <= 0)
                throw new ArgumentOutOfRangeException();

            if (deliveryComboBonus == null)
                throw new ArgumentNullException(nameof(deliveryComboBonus));

            if (speedBonus == null)
                throw new ArgumentNullException(nameof(speedBonus));

            if (distanceBonus == null)
                throw new ArgumentNullException(nameof(distanceBonus));

            if (remainingTimeBonus == null)
                throw new ArgumentNullException(nameof(remainingTimeBonus));

            _deliverySuccessScore = deliverySuccessScore;
            _deliveryComboBonus = deliveryComboBonus;
            _speedBonus = speedBonus;
            _distanceBonus = distanceBonus;
            _remainingTimeBonus = remainingTimeBonus;
        }
    }
}
