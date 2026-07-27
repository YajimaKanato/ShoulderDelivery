namespace ShoulderDelivery.Entity
{
    /// <summary>ステージ内での状態を定義した列挙型</summary>
    public enum StageState
    {
        BeforeCountDown,
        CountDown,
        IsPlaying,
        Cleared,
        TimeUp
    }
}
