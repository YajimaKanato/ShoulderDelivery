namespace ShoulderDelivery.Adapter
{
    /// <summary>画面に描画するオブジェクトに実装するインターフェースのベース</summary>
    public interface IGameView
    {
        /// <summary>コントローラーの有効無効を切り替えるメソッド</summary>
        /// <param name="enabled">コントローラーの有効無効</param>
        void SetControllerEnabled(bool enabled);
    }
}
