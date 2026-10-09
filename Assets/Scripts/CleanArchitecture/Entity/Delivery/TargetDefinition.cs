using ShoulderDerivery.Common;

namespace ShoulderDelivery.Entity
{
    /// <summary>ターゲットの定義を持つ構造体</summary>
    public readonly struct TargetDefinition : IEntity
    {
        /// <summary>ターゲットのID</summary>
        public readonly TargetId Id;
        /// <summary>ターゲットの座標</summary>
        public readonly Coordinates Position;
        /// <summary>求める段ボールの重さ</summary>
        public readonly CardboardWeight RequestedWeight;

        public TargetDefinition(TargetId id, Coordinates position, CardboardWeight requestedWeight)
        {
            Id = id;
            Position = position;
            RequestedWeight = requestedWeight;
        }
    }
}