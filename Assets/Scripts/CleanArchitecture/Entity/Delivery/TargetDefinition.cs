namespace ShoulderDelivery.Entity
{
    /// <summary>ターゲットの定義を持つ構造体</summary>
    public readonly struct TargetDefinition
    {
        /// <summary>ターゲットのID</summary>
        public readonly TargetId Id;
        /// <summary>ターゲットの座標</summary>
        public readonly Vector3 Position;

        public TargetDefinition(TargetId id, Vector3 position)
        {
            Id = id;
            Position = position;
        }
    }
}