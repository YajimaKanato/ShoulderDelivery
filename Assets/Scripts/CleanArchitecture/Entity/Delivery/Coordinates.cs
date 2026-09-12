using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>座標をエンジンに依存しない形で保存するための構造体</summary>
    public readonly struct Coordinates : IEquatable<Coordinates>
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public Coordinates(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Equals(Coordinates other) => X == other.X && Y == other.Y && Z == other.Z;
        public override bool Equals(object obj) => obj is Coordinates other && Equals(other);
        public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        public override string ToString() => $"({X}, {Y}, {Z})";
        public static Coordinates operator -(Coordinates lhs, Coordinates rhs)
        {
            return new Coordinates(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }


        public static float SqrMagnitude(Coordinates vector)
        {
            return (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z);
        }

        public static float Magnitude(Coordinates vector)
        {
            return MathF.Sqrt(SqrMagnitude(vector));
        }
    }
}
