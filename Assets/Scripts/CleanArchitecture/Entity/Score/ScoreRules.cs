using System;
using System.Linq;

namespace ShoulderDelivery.Entity
{
    /// <summary>スコア計算に必要な基本データを持つクラス</summary>
    public sealed class ScoreRules
    {
        readonly int _deliverySuccessScore;
        readonly int[] _deliveryComboBonus;
        readonly SpeedBonusRule _speedBonus;
        readonly int[] _distanceBonus;
        readonly int[] _secondsRemainingBonus;

        /// <summary>配達成功時の基礎スコア</summary>
        public int DeliverySuccessScore => _deliverySuccessScore;
        public SpeedBonusRule SpeedBonusBonus => _speedBonus;

        public ScoreRules(int deliverySuccessScore
            , int[] deliveryComboBonus
            , SpeedBonusRule speedBonus
            , int[] distanceBonus
            , int[] secondsRemainingBonus)
        {
            if (deliverySuccessScore <= 0)
                throw new ArgumentOutOfRangeException();

            if (deliveryComboBonus == null)
                throw new ArgumentNullException(nameof(deliveryComboBonus));

            if (speedBonus == null)
                throw new ArgumentNullException(nameof(speedBonus));

            if (distanceBonus == null)
                throw new ArgumentNullException(nameof(distanceBonus));

            if (secondsRemainingBonus == null)
                throw new ArgumentNullException(nameof(secondsRemainingBonus));

            _deliverySuccessScore = deliverySuccessScore;
            _deliveryComboBonus = deliveryComboBonus;
            _speedBonus = speedBonus;
            _distanceBonus = distanceBonus;
            _secondsRemainingBonus = secondsRemainingBonus;
        }

        /// <summary>
        /// 連続配達成功回数に応じたボーナススコアを取得するメソッド
        /// </summary>
        /// <param name="combo">連続配達成功回数</param>
        /// <returns>ボーナススコア</returns>
        public int DeliveryComboBonus(int combo)
        {
            return combo < _deliveryComboBonus.Length - 1
                ? _deliveryComboBonus[combo]
                : _deliveryComboBonus.Last();
        }
    }
}
