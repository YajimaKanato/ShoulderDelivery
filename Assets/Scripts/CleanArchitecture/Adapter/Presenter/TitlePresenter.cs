using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;
using System.Collections.Generic;

namespace ShoulderDelivery.Adapter
{
    /// <summary>タイトルの出力を表示用のデータに変換するクラス</summary>
    public class TitlePresenter : ITitleOutputPort
    {
        readonly IStageSelectView _stageSelectView;

        public TitlePresenter(IStageSelectView stageSelectView)
        {
            _stageSelectView = stageSelectView;
        }

        public void ShowStageList(IReadOnlyList<StageId> stageIds)
        {
            // ViewModel作成
            var viewModel = new CreateStageSelectViewModel(stageIds);
            _stageSelectView.CreateStageSelectButton(viewModel);
        }

        public void ShowStageInfo(StageDefinition stage)
        {
            // ViewModel作成
            var viewModel = new ShowStageInfoViewModel(stage.Id.Id
                , stage.TimeLimitSeconds
                , stage.RequiredDeliveryCount
                , stage.TargetIds.Count);

            _stageSelectView.ShowStageInfo(viewModel);
        }
    }
}
