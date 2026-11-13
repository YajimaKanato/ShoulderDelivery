using ShoulderDelivery.Common;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームを開始するときに必要な情報を持つDTO</summary>
    public readonly struct StageInfoOutput
    {
        public readonly StageHour StageHour;
        public readonly int TimeLimitSeconds;
        public readonly int RequiredDeliveryCount;

        public StageInfoOutput(StageHour stageHour, int timeLimitSeconds, int requiredDeliveryCount)
        {
            StageHour = stageHour;
            TimeLimitSeconds = timeLimitSeconds;
            RequiredDeliveryCount = requiredDeliveryCount;
        }
    }
}
