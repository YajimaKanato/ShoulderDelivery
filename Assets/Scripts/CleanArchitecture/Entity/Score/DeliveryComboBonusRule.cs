using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>連続配達成功回数に応じたボーナススコアの管理をするクラス</summary>
    public class DeliveryComboBonusRule : IEntity
    {
        readonly DeliveryComboBonus[] _bonusTable;

        public DeliveryComboBonusRule(DeliveryComboBonus[] bonusTable)
        {
            if (bonusTable == null)
                throw new ArgumentNullException(nameof(bonusTable));

            _bonusTable = bonusTable;
        }

        /// <summary>
        /// 連続配達成功回数に応じたボーナススコアを返すメソッド
        /// </summary>
        /// <param name="combo">連続配達成功回数</param>
        /// <returns>ボーナススコア</returns>
        public int ResolveDeliveryComboBonus(int combo)
        {
            var result = 0;
            foreach (var bonus in _bonusTable)
            {
                if (bonus.Combo <= combo)
                {
                    result = bonus.Bonus;
                }
                else
                {
                    break;
                }
            }

            return result;
        }
    }
}
