namespace ShoulderDelivery.Entity
{
    /// <summary>投擲結果を保存する構造体</summary>
    public readonly struct ThrowResult
    {
        public readonly ThrowOutcome Outcome;
        public readonly TargetId? TargetId;
        public readonly Vector3? Position;

        public ThrowResult(ThrowOutcome outcome, TargetId? targetId, Vector3? position)
        {
            Outcome = outcome;
            TargetId = targetId;
            Position = position;
        }
    }
}
