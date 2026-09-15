namespace ShoulderDelivery.Entity
{
    /// <summary>ターゲットの定義を持つ構造体</summary>
    public readonly struct TargetDefinition : IEntity
    {
        /// <summary>ターゲットのID</summary>
        public readonly TargetId Id;
        /// <summary>ターゲットの座標</summary>
        public readonly Coordinates Position;

        public TargetDefinition(TargetId id, Coordinates position)
        {
            Id = id;
            Position = position;
        }
    }
}