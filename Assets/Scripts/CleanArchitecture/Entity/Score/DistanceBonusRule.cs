using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>距離に応じたボーナススコアを管理するクラス</summary>
    public class DistanceBonusRule
    {
        readonly (float distance, int bonus)[] _bonusTable;

        public DistanceBonusRule(params (float, int)[] bonusTable)
        {
            if (bonusTable == null)
                throw new ArgumentNullException(nameof(bonusTable));

            _bonusTable = bonusTable;
        }

        /// <summary>
        /// 距離に応じたスコアを返すメソッド
        /// </summary>
        /// <param name="distance">距離</param>
        /// <returns>距離に応じたスコア</returns>
        public int ResolveDistanceBonus(float distance)
        {
            var result = 0;
            foreach (var bonus in _bonusTable)
            {
                if (bonus.distance * bonus.distance <= distance)
                {
                    // ボーナスラインを超えている場合はスコア更新
                    result = bonus.bonus;
                }
                else
                {
                    // ボーナスラインを下回ったら終了
                    break;
                }
            }

            return result;
        }
    }
}
