namespace ShoulderDelivery.Adapter
{
    /// <summary>配達結果を表示する機能を持つインターフェース</summary>
    public interface IDeliveriedView : IGameView
    {
        /// <summary>配達結果を表示するメソッド</summary>
        /// <param name="score">獲得したスコア</param>
        /// <param name="deliveried">配達成功したかどうか</param>
        void ShowDeliveryResult(string score, string deliveried);
    }
}
