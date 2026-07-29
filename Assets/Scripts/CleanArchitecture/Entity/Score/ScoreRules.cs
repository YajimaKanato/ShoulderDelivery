using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>スコア計算に必要な基本データを持つクラス</summary>
    public sealed class ScoreRules
    {
        readonly int _deliverySuccessScore;
        readonly int[] _deliveryComboBonus;
        readonly int _maxSpeedBonus;
        readonly int _maxDistanceBonus;
        readonly int _secondsRemainingBonus;

        /// <summary>配達成功時の基礎スコア</summary>
        public int DeliverySuccessScore => _deliverySuccessScore;
        /// <summary>連続配達成功によるボーナススコア</summary>
        public int[] DeliveryComboBonus => _deliveryComboBonus;
        /// <summary>速度に応じたボーナススコアの最大値</summary>
        public int MaxSpeedBonus => _maxSpeedBonus;
        /// <summary>距離に応じたボーナススコアの最大値 </summary>
        public int MaxDistanceBonus => _maxDistanceBonus;
        /// <summary>残り時間に応じたボーナススコアの最大値</summary>
        public int SecondsRemainingBonus => _secondsRemainingBonus;

        public ScoreRules(int deliverySuccessScore
            , int[] deliveryComboBonus
            , int maxSpeedBonus
            , int maxDistanceBonus
            , int secondsRemainingBonus)
        {
            if (deliverySuccessScore <= 0
                || maxSpeedBonus <= 0
                || MaxDistanceBonus <= 0
                || secondsRemainingBonus <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _deliverySuccessScore = deliverySuccessScore;
            _deliveryComboBonus = deliveryComboBonus;
            _maxSpeedBonus = maxSpeedBonus;
            _maxDistanceBonus = maxDistanceBonus;
            _secondsRemainingBonus = secondsRemainingBonus;
        }
    }
}
