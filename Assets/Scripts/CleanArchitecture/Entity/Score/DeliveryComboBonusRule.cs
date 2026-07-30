using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>連続配達成功回数に応じたボーナススコアの管理をするクラス</summary>
    public class DeliveryComboBonusRule
    {
        readonly (int combo, int bonus)[] _bonusTable;

        public DeliveryComboBonusRule(params (int, int)[] bonusTable)
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
                if (bonus.combo <= combo)
                {
                    result = bonus.bonus;
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
