namespace ShoulderDelivery.Adapter
{
    /// <summary>配達結果の表示用ViewModel</summary>
    public readonly struct DeliveriedViewModel
    {
        public readonly int TotalScore;
        public readonly int CurrentGetScore;
        /// <summary>今回の配達で獲得した移動速度ボーナス</summary>
        public readonly int SpeedBonus;
        /// <summary>今回の配達で獲得した投擲距離ボーナス</summary>
        public readonly int DistanceBonus;

        public DeliveriedViewModel(int totalScore
            , int currentGetScore
            , int speedBonus
            , int distanceBonus)
        {
            TotalScore = totalScore;
            CurrentGetScore = currentGetScore;
            SpeedBonus = speedBonus;
            DistanceBonus = distanceBonus;
        }
    }
}
