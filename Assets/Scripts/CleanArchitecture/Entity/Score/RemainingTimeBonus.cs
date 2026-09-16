namespace ShoulderDelivery.Entity
{
    /// <summary>残り時間に応じたボーナススコアの要素構造体</summary>
    public readonly struct RemainingTimeBonus
    {
        public readonly int RemainingTime;
        public readonly int Bonus;

        public RemainingTimeBonus(int remainingTime, int bonus)
        {
            RemainingTime = remainingTime;
            Bonus = bonus;
        }

    }
}
