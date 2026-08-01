using System;
using System.Collections.Generic;

namespace ShoulderDelivery.Entity
{
    /// <summary>ステージの定義を持つクラス</summary>
    public sealed class StageDefinition
    {
        readonly StageId _id;
        readonly int _countDownSeconds;
        readonly int _timeLimitSeconds;
        readonly int _requiredDeliveryCount;
        readonly List<TargetId> _targetIds;
        readonly ScoreRules _scoreRules;

        public StageId Id => _id;
        public int CountDownSeconds => _countDownSeconds;
        public int TimeLimitSeconds => _timeLimitSeconds;
        public int RequiredDeliveryCount => _requiredDeliveryCount;
        public IReadOnlyList<TargetId> TargetIds => _targetIds;
        public ScoreRules ScoreRules => _scoreRules;

        public StageDefinition(StageId id
            , int countDownSeconds
            , int timeLimitSeconds
            , int requiredDeliveryCount
            , List<TargetId> targetIds
            , ScoreRules scoreRules)
        {
            if (countDownSeconds < 0)
                throw new ArgumentOutOfRangeException(nameof(countDownSeconds));

            if (timeLimitSeconds < 0)
                throw new ArgumentOutOfRangeException(nameof(timeLimitSeconds));

            if (requiredDeliveryCount < 0)
                throw new ArgumentOutOfRangeException(nameof(requiredDeliveryCount));

            if (targetIds == null)
                throw new ArgumentNullException(nameof(targetIds));

            if (scoreRules == null)
                throw new ArgumentNullException(nameof(scoreRules));

            _id = id;
            _countDownSeconds = countDownSeconds;
            _timeLimitSeconds = timeLimitSeconds;
            _requiredDeliveryCount = requiredDeliveryCount;
            _targetIds = new List<TargetId>(targetIds);
            _scoreRules = scoreRules;
        }
    }
}
