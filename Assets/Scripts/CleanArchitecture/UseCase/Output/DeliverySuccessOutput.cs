using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>配達成功結果を持つDTO</summary>
    public readonly struct DeliverySuccessOutput
    {
        public readonly ScoreBreakdown ScoreBreakdown;
        public readonly int Score;

        public DeliverySuccessOutput(ScoreBreakdown scoreBreakdown, int score)
        {
            ScoreBreakdown = scoreBreakdown;
            Score = score;
        }
    }
}