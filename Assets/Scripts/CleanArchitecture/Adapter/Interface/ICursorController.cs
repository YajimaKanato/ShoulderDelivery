namespace ShoulderDelivery.Adapter
{
    /// <summary>カーソルの表示を管理する機能を持つインターフェース</summary>
    public interface ICursorController
    {
        /// <summary>カーソルを表示するメソッド</summary>
        void ShowCursor();

        /// <summary>カーソルを隠すメソッド</summary>
        void HideCursor();
    }
}
