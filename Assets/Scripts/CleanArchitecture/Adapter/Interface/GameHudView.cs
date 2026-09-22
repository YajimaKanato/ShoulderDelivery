namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲームの状態を毎フレーム描画する機能を持つ抽象クラス </summary>
    public abstract class GameHudView : GameView
    {
        /// <summary>ゲームの状態を描画するメソッド</summary>
        /// <para>毎フレーム呼ばれる</para>
        /// <param name="remainingTime">残り時間</param>
        /// <param name="remainingDeliveryCount">残り配達数</param>
        /// <param name="score">スコア</param>
        public abstract void ShowHud(string remainingTime, string remainingDeliveryCount, string score);
    }
}
