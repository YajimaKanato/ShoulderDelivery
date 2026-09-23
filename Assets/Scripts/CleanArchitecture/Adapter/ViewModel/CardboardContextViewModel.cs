namespace ShoulderDelivery.Adapter
{
    /// <summary>段ボールの投擲結果用のViewModel</summary>
    public readonly struct CardboardContextViewModel
    {
        public readonly string Context;

        public CardboardContextViewModel(string context)
        {
            Context = context;
        }
    }
}
