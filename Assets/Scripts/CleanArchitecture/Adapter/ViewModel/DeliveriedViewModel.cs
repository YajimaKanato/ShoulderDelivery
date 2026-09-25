namespace ShoulderDelivery.Adapter
{
    /// <summary>配達結果の表示用ViewModel</summary>
    public readonly struct DeliveriedViewModel
    {
        public readonly int TotalScore;
        public readonly int CurrentGetScore;

        public DeliveriedViewModel(int totalScore, int currentGetScore)
        {
            TotalScore = totalScore;
            CurrentGetScore = currentGetScore;
        }
    }
}
