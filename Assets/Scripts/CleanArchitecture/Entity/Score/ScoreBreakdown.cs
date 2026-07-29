using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>計算のタイミングごとのスコアを持つ構造体</summary>
    public readonly struct ScoreBreakdown
    {
        /// <summary>配達成功によるスコア</summary>
        public readonly int DeliverySuccessScore;
        /// <summary>連続配達成功によるスコア</summary>
        public readonly int DeliveryComboBonus;
        /// <summary>移動速度によるボーナススコア</summary>
        public readonly int SpeedBonus;
        /// <summary>投擲距離によるボーナススコア</summary>
        public readonly int DistanceBonus;
        /// <summary>残り時間によるボーナススコア</summary>
        public readonly int SecondsRemainingBonus;

        /// <summary>合計</summary>
        public int Total => Math.Max(0,
            DeliverySuccessScore
            + DeliveryComboBonus
            + SpeedBonus
            + DistanceBonus
            + SecondsRemainingBonus);

        public ScoreBreakdown(int deliverySuccessScore
            , int deliveryComboBonus
            , int speedBonus
            , int distanceBonus
            , int secondsRemainingBonus)
        {
            DeliverySuccessScore = deliverySuccessScore;
            DeliveryComboBonus = deliveryComboBonus;
            SpeedBonus = speedBonus;
            DistanceBonus = distanceBonus;
            SecondsRemainingBonus = secondsRemainingBonus;
        }
    }
}
