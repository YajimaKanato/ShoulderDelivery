namespace ShoulderDelivery.Adapter
{
    /// <summary>ステージ情報を持つViewModel</summary>
    public readonly struct ShowStageInfoViewModel
    {
        public readonly string StageName;
        public readonly int TimeLimitSeconds;
        public readonly int RequiredDeliveryCount;
        public readonly int TargetCount;

        public ShowStageInfoViewModel(string stageName
            , int timeLimitSeconds
            , int requiredDeliveryCount
            , int targetCount)
        {
            StageName = stageName;
            TimeLimitSeconds = timeLimitSeconds;
            RequiredDeliveryCount = requiredDeliveryCount;
            TargetCount = targetCount;
        }
    }
}
