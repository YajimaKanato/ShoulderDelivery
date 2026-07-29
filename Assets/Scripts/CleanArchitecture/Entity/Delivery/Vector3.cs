using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>座標をエンジンに依存しない形で保存するための構造体</summary>
    public readonly struct Vector3 : IEquatable<Vector3>
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Equals(Vector3 other) => X == other.X && Y == other.Y && Z == other.Z;
        public override bool Equals(object obj) => obj is Vector3 other && Equals(other);
        public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        public override string ToString() => $"({X}, {Y}, {Z})";
    }
}
