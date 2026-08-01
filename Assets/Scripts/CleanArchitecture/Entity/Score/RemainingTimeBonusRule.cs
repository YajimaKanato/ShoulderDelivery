using System;

namespace ShoulderDelivery.Entity
{
    public class RemainingTimeBonusRule
    {
        readonly (int remainigTime, int bonus)[] _bonusTable;

        public RemainingTimeBonusRule(params (int, int)[] bonusTable)
        {
            if (bonusTable == null)
                throw new ArgumentNullException(nameof(bonusTable));

            _bonusTable = bonusTable;
        }

        /// <summary>
        /// 残り時間に応じたボーナススコアを返すメソッド
        /// </summary>
        /// <param name="remainingTime">残り時間</param>
        /// <returns>ボーナススコア</returns>
        public int ResolveRemainigTimeBonus(float remainingTime)
        {
            var result = 0;
            foreach (var bonus in _bonusTable)
            {
                if (bonus.remainigTime <= remainingTime)
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
