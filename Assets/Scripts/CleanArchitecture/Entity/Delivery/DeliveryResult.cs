namespace ShoulderDelivery.Entity
{
    /// <summary>配達結果を保存する構造体</summary>
    public readonly struct DeliveryResult
    {
        public readonly ThrowOutcome Outcome;
        public readonly TargetDefinition? TargetDefinition;
        public readonly int Combo;

        public DeliveryResult(ThrowOutcome outcome, TargetDefinition? targetDefinition, int combo)
        {
            Outcome = outcome;
            TargetDefinition = targetDefinition;
            Combo = combo;
        }
    }
}
