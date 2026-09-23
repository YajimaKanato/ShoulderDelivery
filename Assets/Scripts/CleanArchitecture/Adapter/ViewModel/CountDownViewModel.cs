namespace ShoulderDelivery.Adapter
{
    /// <summary>カウントダウンの表示用ViewModel</summary>
    public readonly struct CountDownViewModel
    {
        public readonly float RemainingSeconds;

        public CountDownViewModel(float remainingSeconds)
        {
            RemainingSeconds = remainingSeconds;
        }
    }
}
