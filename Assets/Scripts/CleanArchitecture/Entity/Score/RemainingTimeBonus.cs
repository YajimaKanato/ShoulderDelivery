using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>残り時間に応じたボーナススコアの要素構造体</summary>
    [Serializable]
    public struct RemainingTimeBonus
    {
        public int RemainingTime;
        public int Bonus;
    }
}
