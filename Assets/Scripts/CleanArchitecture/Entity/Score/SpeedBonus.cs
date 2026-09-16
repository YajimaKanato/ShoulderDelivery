namespace ShoulderDelivery.Entity
{
    /// <summary>投擲時の移動速度に応じたボーナススコアの要素構造体</summary>
    public readonly struct SpeedBonus
    {
        public readonly float Speed;
        public readonly int Bonus;

        public SpeedBonus(float speed, int bonus)
        {
            Speed = speed;
            Bonus = bonus;
        }
    }
}
