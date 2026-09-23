namespace ShoulderDelivery.Adapter
{
    /// <summary>Hudの表示用モデル</summary>
    public readonly struct HudViewModel
    {
        public readonly float RemainingTime;
        public readonly int RemainingDeliveryCount;
        public readonly string Score;

        public HudViewModel(float remainingTime, int remainingDeliveryCount, string score)
        {
            RemainingTime = remainingTime;
            RemainingDeliveryCount = remainingDeliveryCount;
            Score = score;
        }
    }
}
