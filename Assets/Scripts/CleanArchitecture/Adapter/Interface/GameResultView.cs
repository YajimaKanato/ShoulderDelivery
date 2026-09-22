namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲームの結果を表示する機能を持つ抽象クラス</summary>
    public abstract class GameResultView : GameView
    {
        /// <summary>ゲームの結果を表示するメソッド</summary>
        /// <param name="totalScore">合計スコア</param>
        /// <param name="deliveryCount">合計配達数</param>
        /// <param name="remainingTime">残り時間</param>
        public abstract void ShowFailed(string totalScore, string deliveryCount, string remainingTime);

        /// <summary>ゲームの結果を表示するメソッド</summary>
        /// <param name="totalScore">合計スコア</param>
        /// <param name="deliveryCount">合計配達数</param>
        /// <param name="remainingTime">残り時間</param>
        public abstract void ShowClear(string totalScore, string deliveryCount, string remainingTime);
    }
}
