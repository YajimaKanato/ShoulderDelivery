namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲームの状態を毎フレーム描画する機能を持つインターフェース</summary>
    public interface IGameHudView
    {
        /// <summary>ゲームの状態を描画するメソッド</summary>
        /// <para>毎フレーム呼ばれる</para>
        /// <param name="viewModel">ViewModel</param>
        void ShowHud(HudViewModel viewModel);

        /// <summary>配達時の状態を描画するメソッド</summary>
        /// <param name="viewModel">ViewModel</param>
        void ShowDelivery(DeliveryViewModel viewModel);
    }
}
