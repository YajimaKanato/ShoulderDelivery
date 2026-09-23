namespace ShoulderDelivery.Adapter
{
    /// <summary>ステージ情報表示用ViewModel</summary>
    public readonly struct StageInfoViewModel
    {
        public readonly float TimeLimitSeconds;
        public readonly int RequiredDeliveryCount;

        public StageInfoViewModel(float timeLimitSeconds, int requiredDeliveryCount)
        {
            TimeLimitSeconds = timeLimitSeconds;
            RequiredDeliveryCount = requiredDeliveryCount;
        }
    }
}
