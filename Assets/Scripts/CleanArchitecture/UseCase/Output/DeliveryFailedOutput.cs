using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>配達失敗結果を持つDTO</summary>
    public readonly struct DeliveryFailedOutput
    {
        /// <summary>現在のスコア</summary>
        public readonly int Score;
        /// <summary>配達の内訳</summary>
        public readonly DeliveryBreakdown DeliveryBreakdown;

        DeliveryFailedOutput(int score, DeliveryBreakdown deliveryBreakdown)
        {
            Score = score;
            DeliveryBreakdown = deliveryBreakdown;
        }

        /// <summary>
        /// 配達失敗のDTOを生成するメソッド
        /// </summary>
        /// <param name="score">現在のスコア</param>
        /// <param name="deliveryBreakdown">配達の内訳</param>
        /// <returns>配達失敗のDTO</returns>
        internal static DeliveryFailedOutput Missed(int score, DeliveryBreakdown deliveryBreakdown)
        {
            return new DeliveryFailedOutput(score, deliveryBreakdown);
        }
    }
}
