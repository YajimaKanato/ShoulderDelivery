namespace ShoulderDelivery.Entity
{
    /// <summary>投擲距離に応じたボーナススコアの要素構造体</summary>
    public readonly struct DistanceBonus
    {
        public readonly float Distance;
        public readonly int Bonus;

        public DistanceBonus(float distance, int bonus)
        {
            Distance = distance;
            Bonus = bonus;
        }
    }
}
