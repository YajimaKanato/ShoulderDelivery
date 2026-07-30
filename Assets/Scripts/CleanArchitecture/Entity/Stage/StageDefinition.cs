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

        public StageId Id => _id;
        public int CountDownSeconds => _countDownSeconds;
        public int TimeLimitSeconds => _timeLimitSeconds;
        public int RequiredDeliveryCount=> _requiredDeliveryCount;
        public IReadOnlyList<TargetId> TargetIds => _targetIds;
    }
}
