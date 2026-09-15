using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>連続配達成功回数に応じたボーナススコアの要素構造体</summary>
    [Serializable]
    public struct DeliveryComboBonus
    {
        public int Combo;
        public int Bonus;
    }
}
