using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>段ボールのIDを定義した構造体</summary>
    public readonly struct CardboardId : IEquatable<CardboardId>
    {
        public readonly int Id;

        public CardboardId(int id)
        {
            Id = id;
        }

        public bool Equals(CardboardId other) => Id == other.Id;
        public override bool Equals(object obj) => obj is CardboardId other && Equals(other);
        public override int GetHashCode() => Id;
        public override string ToString() => Id.ToString();
    }
}
