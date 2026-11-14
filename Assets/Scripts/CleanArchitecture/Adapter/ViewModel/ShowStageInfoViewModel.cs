using ShoulderDelivery.Common;

namespace ShoulderDelivery.Adapter
{
    /// <summary>ステージ情報を持つViewModel</summary>
    public readonly struct ShowStageInfoViewModel
    {
        public readonly string StageName;
        public readonly StageHour StageHour;
        public readonly int TimeLimitSeconds;
        public readonly int RequiredDeliveryCount;
        public readonly int TargetCount;

        public ShowStageInfoViewModel(string stageName
            , StageHour stageHour
            , int timeLimitSeconds
            , int requiredDeliveryCount
            , int targetCount)
        {
            StageName = stageName;
            StageHour = stageHour;
            TimeLimitSeconds = timeLimitSeconds;
            RequiredDeliveryCount = requiredDeliveryCount;
            TargetCount = targetCount;
        }
    }
}
