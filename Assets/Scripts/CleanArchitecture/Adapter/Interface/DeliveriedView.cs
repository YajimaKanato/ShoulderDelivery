namespace ShoulderDelivery.Adapter
{
    /// <summary>配達結果を表示する機能を持つ抽象クラス</summary>
    public abstract class DeliveriedView : GameView
    {
        /// <summary>配達結果を表示するメソッド</summary>
        /// <param name="score">獲得したスコア</param>
        public abstract void ShowDeliverySucceededResult(string score);

        /// <summary>配達結果を表示するメソッド</summary>
        public abstract void ShowDeliveryFailedResult();
    }
}
