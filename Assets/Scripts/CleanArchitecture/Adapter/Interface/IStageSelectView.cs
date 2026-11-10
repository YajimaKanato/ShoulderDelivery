namespace ShoulderDelivery.Adapter
{
    /// <summary>タイトルのステージ管理の機能を持つインターフェース</summary>
    public interface IStageSelectView
    {
        /// <summary>ステージ選択ボタンを作成するメソッド</summary>
        /// <param name="viewModel">ViewModel</param>
        void CreateStageSelectButton(CreateStageSelectViewModel viewModel);

        /// <summary>ステージ情報を表示するメソッド</summary>
        /// <param name="viewModel">ViewModel</param>
        void ShowStageInfo(ShowStageInfoViewModel viewModel);
    }
}
