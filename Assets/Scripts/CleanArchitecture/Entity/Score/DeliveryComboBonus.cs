namespace ShoulderDelivery.Entity
{
    /// <summary>連続配達成功回数に応じたボーナススコアの要素構造体</summary>
    public readonly struct DeliveryComboBonus
    {
        public readonly int Combo;
        public readonly int Bonus;

        public DeliveryComboBonus(int combo, int bonus)
        {
            Combo = combo;
            Bonus = bonus;
        }

    }
}
