using ShoulderDelivery.Entity;

namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲーム失敗した時の情報表示用DTO</summary>
    public readonly struct GameFailedViewModel
    {
        public readonly int TotalScore;
        public readonly int RemainingTimeBonus;
        public readonly int RequiredDeliveryCount;
        public readonly int DeliveryCount;
        public readonly float RemainingTime;
        public readonly ScoreRank Rank;

        public GameFailedViewModel(int totalScore, int requiredDeliveryCount, int deliveryCount, float remainingTime, ScoreRank rank, int remainingTimeBonus = 0)
        {
            TotalScore = totalScore;
            RemainingTimeBonus = remainingTimeBonus;
            RequiredDeliveryCount = requiredDeliveryCount;
            DeliveryCount = deliveryCount;
            RemainingTime = remainingTime;
            Rank = rank;
        }
    }
}
