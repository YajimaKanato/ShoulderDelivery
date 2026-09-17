namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲームの状態を毎フレーム描画する機能を持つインターフェース</summary>
    public interface IGameHudView : IGameView
    {
        /// <summary>ゲームの状態を描画するメソッド</summary>
        /// <para>毎フレーム呼ばれる</para>
        /// <param name="remainingTime">残り時間</param>
        /// <param name="remainingDeliveryCount">残り配達数</param>
        /// <param name="score">スコア</param>
        void ShowHud(string remainingTime, string remainingDeliveryCount, string score);
    }
}
