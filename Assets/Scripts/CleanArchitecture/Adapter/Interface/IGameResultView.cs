namespace ShoulderDelivery.Adapter
{
    /// <summary>ゲームの結果を表示する機能を持つインターフェース</summary>
    public interface IGameResultView
    {
        /// <summary>ゲームの失敗を表示するメソッド</summary>
        /// <param name="viewModel">ViewModel</param>
        void ShowFailed(GameFailedViewModel viewModel);

        /// <summary>ゲームの成功を表示するメソッド</summary>
        /// <param name="viewModel">ViewModel</param>
        void ShowClear(GameClearViewModel viewModel);
    }
}
