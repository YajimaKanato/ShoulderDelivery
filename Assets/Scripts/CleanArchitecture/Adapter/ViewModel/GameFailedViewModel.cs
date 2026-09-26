namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲーム失敗した時の情報表示用DTO</summary>
    public readonly struct GameFailedViewModel
    {
        public readonly int TotalScore;
        public readonly int RequiredDeliveryCount;
        public readonly int DeliveryCount;
        public readonly float ClearTime;

        public GameFailedViewModel(int totalScore, int requiredDeliveryCount, int deliveryCount, float clearTime)
        {
            TotalScore = totalScore;
            RequiredDeliveryCount = requiredDeliveryCount;
            DeliveryCount = deliveryCount;
            ClearTime = clearTime;
        }
    }
}
