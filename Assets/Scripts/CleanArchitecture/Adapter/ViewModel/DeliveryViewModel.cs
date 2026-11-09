namespace ShoulderDelivery.Adapter
{
    /// <summary>配達時の情報の表示用ViewModel</summary>
    public readonly struct DeliveryViewModel
    {
        public readonly int Score;
        public readonly int Combo;
        public readonly int DeliveryCount;
        public readonly int RequiredCount;

        public DeliveryViewModel(int score, int combo, int deliveryCount, int requiredCount)
        {
            Score = score;
            Combo = combo;
            DeliveryCount = deliveryCount;
            RequiredCount = requiredCount;
        }
    }
}
