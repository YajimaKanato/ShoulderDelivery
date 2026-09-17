namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲームの結果を表示する機能を持つインターフェース</summary>
    public interface IGameResult : IGameView
    {
        /// <summary>ゲームの結果を表示するメソッド</summary>
        /// <param name="totalScore">合計スコア</param>
        /// <param name="deliveryCount">合計配達数</param>
        /// <param name="remainingTime">残り時間</param>
        /// <param name="isQuotaMet">ノルマ達成状況</param>
        void ShowResult(string totalScore, string deliveryCount, string remainingTime, QuotaType isQuotaMet);
    }
}
