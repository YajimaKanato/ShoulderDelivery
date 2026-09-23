namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲームの結果を表示する機能を持つインターフェース</summary>
    public interface IGameResultView
    {
        /// <summary>ゲームの失敗を表示するメソッド</summary>
        /// <param name="totalScore">合計スコア</param>
        /// <param name="deliveryCount">合計配達数</param>
        /// <param name="remainingTime">残り時間</param>
        void ShowFailed(string totalScore, string deliveryCount, string remainingTime);

        /// <summary>ゲームの成功を表示するメソッド</summary>
        /// <param name="totalScore">合計スコア</param>
        /// <param name="deliveryCount">合計配達数</param>
        /// <param name="remainingTime">残り時間</param>
        void ShowClear(string totalScore, string deliveryCount, string remainingTime);
    }
}
