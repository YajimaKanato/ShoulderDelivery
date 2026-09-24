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
    }
}