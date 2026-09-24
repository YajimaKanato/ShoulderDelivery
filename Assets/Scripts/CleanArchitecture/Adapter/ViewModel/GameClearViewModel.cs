namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲームクリアした時の情報表示用DTO</summary>
    public readonly struct GameClearViewModel
    {
        public readonly int Total;
        public readonly int RequiredDeliveryCount;
        public readonly int DeliveryCount;
        public readonly float RemainingTime;

        public GameClearViewModel(int total, int requiredDeliveryCount, int deliveryCount, float remainingTime)
        {
            Total = total;
            RequiredDeliveryCount = requiredDeliveryCount;
            DeliveryCount = deliveryCount;
            RemainingTime = remainingTime;
        }
    }
}
