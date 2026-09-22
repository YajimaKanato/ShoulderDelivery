namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームを開始するときに必要な情報を持つDTO</summary>
    public readonly struct StartGameOutput
    {
        public readonly int TimeLimitSeconds;
        public readonly int RequiredDeliveryCount;

        public StartGameOutput(int timeLimitSeconds, int requiredDeliveryCount)
        {
            TimeLimitSeconds = timeLimitSeconds;
            RequiredDeliveryCount = requiredDeliveryCount;
        }
    }
}
