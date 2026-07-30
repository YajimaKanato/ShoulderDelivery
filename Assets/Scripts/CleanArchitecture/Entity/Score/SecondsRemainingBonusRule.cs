using System;

namespace ShoulderDelivery.Entity
{
    public class SecondsRemainingBonusRule
    {
        readonly (int remainigSeconds, int bonus)[] _bonusTable;

        public SecondsRemainingBonusRule(params (int, int)[] bonusTable)
        {
            if (bonusTable == null)
                throw new ArgumentNullException(nameof(bonusTable));

            _bonusTable = bonusTable;
        }

        /// <summary>
        /// 残り時間に応じたボーナススコアを返すメソッド
        /// </summary>
        /// <param name="remainingSeconds">残り時間</param>
        /// <returns>ボーナススコア</returns>
        public int ResolveSecondsRemainigBonus(float remainingSeconds)
        {
            var result = 0;
            foreach (var bonus in _bonusTable)
            {
                if (bonus.remainigSeconds <= remainingSeconds)
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
