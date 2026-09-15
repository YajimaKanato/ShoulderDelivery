using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>投擲時の移動速度に応じたボーナススコアの要素構造体</summary>
    [Serializable]
    public struct SpeedBonus
    {
        public float Speed;
        public int Bonus;
    }
}
