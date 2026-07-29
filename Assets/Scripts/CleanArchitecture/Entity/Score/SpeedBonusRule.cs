using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>移動速度に応じたボーナススコアの管理をするクラス</summary>
    public class SpeedBonusRule
    {
        readonly (float speed, int bonus)[] _bonusTable;

        public SpeedBonusRule(params (float, int)[] bonusTable)
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
        public int ResolveSpeedBonus(float speed)
        {
            var result = 0;
            foreach (var bonus in _bonusTable)
            {
                if (bonus.speed <= speed)
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
