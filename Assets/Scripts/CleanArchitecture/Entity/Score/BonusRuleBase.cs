using System;

namespace ShoulderDelivery.Entity
{
    public abstract class BonusRuleBase<TTable, TValue> where TTable : struct where TValue : struct
    {
        protected readonly TTable[] _bonusTable;

        public BonusRuleBase(params TTable[] bonusTable)
        {
            if (bonusTable == null)
                throw new ArgumentNullException(nameof(bonusTable));

            _bonusTable = bonusTable;
        }

        /// <summary>
        /// 移動速度に応じたスコアを返すメソッド
        /// </summary>
        /// <param name="speed">移動速度</param>
        /// <returns>移動速度に応じたスコア</returns>
        public abstract int ResolveSpeedBonus(TValue speed);
    }
}
