using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>配達成功結果を持つDTO</summary>
    public readonly struct DeliverySuccessOutput
    {
        /// <summary>今回獲得したスコアの内訳</summary>
        public readonly ScoreBreakdown ScoreBreakdown;
        /// <summary>現在のスコア</summary>
        public readonly int Score;
        /// <summary>配達の内訳</summary>
        public readonly DeliveryBreakdown DeliveryBreakdown;

        public DeliverySuccessOutput(ScoreBreakdown scoreBreakdown, int score, DeliveryBreakdown deliveryBreakdown)
        {
            ScoreBreakdown = scoreBreakdown;
            Score = score;
            DeliveryBreakdown = deliveryBreakdown;
        }

        /// <summary>
        /// 配達成功のDTOを生成するメソッド
        /// </summary>
        /// <param name="scoreBreakdown">スコア内訳</param>
        /// <param name="score">スコア</param>
        /// <param name="deliveryBreakdown">配達内訳</param>
        /// <returns>配達成功のDTO</returns>
        internal static DeliverySuccessOutput Delivered(ScoreBreakdown scoreBreakdown, int score, DeliveryBreakdown deliveryBreakdown)
        {
            return new DeliverySuccessOutput(scoreBreakdown, score, deliveryBreakdown);
        }
    }
}