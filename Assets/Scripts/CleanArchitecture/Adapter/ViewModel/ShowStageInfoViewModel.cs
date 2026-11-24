using ShoulderDelivery.Common;
using ShoulderDelivery.Entity;

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
        public readonly int? BestScore;
        public readonly ScoreRank? BestRank;
        public readonly int MinimumS;
        public readonly int MinimumA;
        public readonly int MinimumB;
        public readonly int MinimumC;

        public ShowStageInfoViewModel(string stageName
            , StageHour stageHour
            , int timeLimitSeconds
            , int requiredDeliveryCount
            , int targetCount
            , int? bestScore
            , ScoreRank? bestRank
            , int minimumS
            , int minimumA
            , int minimumB
            , int minimumC)
        {
            StageName = stageName;
            StageHour = stageHour;
            TimeLimitSeconds = timeLimitSeconds;
            RequiredDeliveryCount = requiredDeliveryCount;
            TargetCount = targetCount;
            BestScore = bestScore;
            BestRank = bestRank;
            MinimumS = minimumS;
            MinimumA = minimumA;
            MinimumB = minimumB;
            MinimumC = minimumC;
        }
    }
}
