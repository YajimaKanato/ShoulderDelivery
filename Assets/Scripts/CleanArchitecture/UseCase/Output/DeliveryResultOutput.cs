using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>配達結果を持つDTO</summary>
    public readonly struct DeliveryResultOutput
    {
        public readonly ScoreBreakdown? ScoreBreakdown;
        public readonly int Score;

        public DeliveryResultOutput(ScoreBreakdown? scoreBreakdown, int score)
        {
            ScoreBreakdown = scoreBreakdown;
            Score = score;
        }
    }
}
