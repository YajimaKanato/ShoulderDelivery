namespace ShoulderDelivery.Entity
{
    /// <summary>配達結果を保存する構造体</summary>
    public readonly struct DeliveryResult
    {
        public readonly TargetDefinition TargetDefinition;
        public readonly int Combo;

        public DeliveryResult(TargetDefinition targetDefinition, int combo)
        {
            TargetDefinition = targetDefinition;
            Combo = combo;
        }
    }
}
