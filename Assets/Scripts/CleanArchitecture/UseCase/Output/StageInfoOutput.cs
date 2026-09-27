namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームを開始するときに必要な情報を持つDTO</summary>
    public readonly struct StageInfoOutput
    {
        public readonly int TimeLimitSeconds;
        public readonly int RequiredDeliveryCount;

        public StageInfoOutput(int timeLimitSeconds, int requiredDeliveryCount)
        {
            TimeLimitSeconds = timeLimitSeconds;
            RequiredDeliveryCount = requiredDeliveryCount;
        }
    }
}
