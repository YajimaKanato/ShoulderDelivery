using ShoulderDelivery.Common;

namespace ShoulderDelivery.Adapter
{
    /// <summary>ステージ情報表示用ViewModel</summary>
    public readonly struct StageInfoViewModel
    {
        public readonly StageHour StageHour;
        public readonly float TimeLimitSeconds;
        public readonly int RequiredDeliveryCount;

        public StageInfoViewModel(StageHour stageHour, float timeLimitSeconds, int requiredDeliveryCount)
        {
            StageHour = stageHour;
            TimeLimitSeconds = timeLimitSeconds;
            RequiredDeliveryCount = requiredDeliveryCount;
        }
    }
}
