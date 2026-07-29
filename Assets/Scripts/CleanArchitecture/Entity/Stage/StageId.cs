using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>ステージのIDを定義した構造体</summary>
    public readonly struct StageId : IEquatable<StageId>
    {
        /// <summary>ステージID</summary>
        public readonly string Id;

        public StageId(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("StageId is required.", nameof(id));
            }

            Id = id;
        }

        // 等値演算子を使えるようにする
        public bool Equals(StageId other) => Id == other.Id;
        public override bool Equals(object obj) => obj is StageId other && Equals(other);
        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString() => Id;
    }
}
