namespace ShoulderDelivery.Adapter
{
    /// <summary>カウントダウンを表示する機能を持つインターフェース</summary>
    public interface IGameCountDownView
    {
        /// <summary>カウントダウンを表示するメソッド</summary>
        /// <param name="viewModel">ViewModel</param>
        void ShowCountDown(CountDownViewModel viewModel);
    }
}
