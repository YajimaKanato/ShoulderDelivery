namespace ShoulderDelivery.Adapter
{
    /// <summary>段ボールの投擲結果を表示する機能を持つインターフェース</summary>
    public interface ICardboardContextView
    {
        /// <summary>投擲成功を表示するメソッド</summary>
        /// <param name="context">投擲結果</param>
        void ShowCardboardAccepted(CardboardContextViewModel context);

        /// <summary>投擲失敗を表示するメソッド</summary>
        /// <param name="context">投擲結果</param>
        void ShowCardboardRejected(CardboardContextViewModel context);
    }
}
