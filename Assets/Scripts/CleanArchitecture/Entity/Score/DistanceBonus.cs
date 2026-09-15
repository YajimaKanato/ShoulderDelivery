using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>投擲距離に応じたボーナススコアの要素構造体</summary>
    [Serializable]
    public struct DistanceBonus
    {
        public float Distance;
        public int Bonus;
    }
}
