namespace ShoulderDelivery.Adapter
{
    /// <summary>Hudの表示用モデル</summary>
    public readonly struct HudViewModel
    {
        public readonly float RemainingTime;
        public readonly int Score;

        public HudViewModel(float remainingTime, int score)
        {
            RemainingTime = remainingTime;
            Score = score;
        }
    }
}
