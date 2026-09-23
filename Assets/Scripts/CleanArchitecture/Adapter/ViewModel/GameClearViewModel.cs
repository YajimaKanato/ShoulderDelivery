namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲームクリアした時の情報表示用DTO</summary>
    public readonly struct GameClearViewModel
    {
        public readonly int Total;
        public readonly int DeliveryCount;
        public readonly float RemainingTime;
    }
}
