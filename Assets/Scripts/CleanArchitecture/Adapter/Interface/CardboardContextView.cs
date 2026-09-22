namespace ShoulderDelivery.Adapter
{
    /// <summary>段ボールの投擲結果を表示する機能を持つ抽象クラス</summary>
    public abstract class CardboardContextView : GameView
    {
        /// <summary>投擲結果を表示するメソッド</summary>
        /// <param name="context">投擲結果</param>
        public abstract void ShowCardboardAccepted(string context);

        /// <summary>投擲結果を表示するメソッド</summary>
        /// <param name="context">投擲結果</param>
        public abstract void ShowCardboardRejected(string context);
    }
}
