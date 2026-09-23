namespace ShoulderDelivery.Adapter
{
    /// <summary>ステージの情報を表示する機能を持つインターフェース</summary>
    public interface IStageInfoView
    {
        /// <summary>ステージの情報を表示するメソッド</summary>
        /// <param name="viewModel">ViewModel</param>
        void ShowStageInfo(StageInfoViewModel viewModel);
    }
}
