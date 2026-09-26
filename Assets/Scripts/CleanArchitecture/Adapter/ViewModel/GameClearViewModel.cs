namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲームクリアした時の情報表示用DTO</summary>
    public readonly struct GameClearViewModel
    {
        public readonly int TotalScore;
        public readonly int RequiredDeliveryCount;
        public readonly int DeliveryCount;
        public readonly float ClearTime;

        public GameClearViewModel(int totalScore, int requiredDeliveryCount, int deliveryCount, float clearTime)
        {
            TotalScore = totalScore;
            RequiredDeliveryCount = requiredDeliveryCount;
            DeliveryCount = deliveryCount;
            ClearTime = clearTime;
        }
    }
}
