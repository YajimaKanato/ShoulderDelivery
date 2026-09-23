namespace ShoulderDelivery.UseCase
{
    /// <summary>配達失敗結果を持つDTO</summary>
    public readonly struct DeliveryFailedOutput
    {
        /// <summary>現在のスコア</summary>
        public readonly int Score;

        public DeliveryFailedOutput(int score)
        {
            Score = score;
        }
    }
}
