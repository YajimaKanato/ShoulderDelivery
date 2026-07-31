using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>目的地のIDを定義した構造体</summary>
    public readonly struct TargetId : IEquatable<TargetId>
    {
        public readonly string Id;

        public TargetId(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("TargetId is required.", nameof(id));
            }

            Id = id;
        }

        public bool Equals(TargetId other) => Id == other.Id;
        public override bool Equals(object obj) => obj is TargetId other && Equals(other);
        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString() => Id;
        public static bool operator ==(TargetId left, TargetId right) => left.Equals(right);
        public static bool operator !=(TargetId left, TargetId right) => !left.Equals(right);
    }
}
