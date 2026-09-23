namespace ShoulderDelivery.Adapter
{
    /// <summary>配達結果の表示用ViewModel</summary>
    public readonly struct DeliveriedViewModel
    {
        public readonly string Score;

        public DeliveriedViewModel(string score)
        {
            Score = score;
        }
    }
}
