using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;
using System.Collections.Generic;
using System.Threading;

namespace ShoulderDelivery.Adapter
{
    /// <summary>タイトルの出力を表示用のデータに変換するクラス</summary>
    public class TitlePresenter : ITitleOutputPort
    {
        readonly IStageSelectView _stageSelectView;
        CancellationTokenSource _cts;

        public TitlePresenter(IStageSelectView stageSelectView)
        {
            _stageSelectView = stageSelectView;
        }

        public void ShowStageList(IReadOnlyList<(StageId Id, bool IsCleared)> stages)
        {
            CancelStageList();

            // ViewModel作成
            var viewModel = new CreateStageSelectViewModel(stages);
            _cts = new CancellationTokenSource();
            _stageSelectView.CreateStageSelectButton(viewModel, _cts.Token);
        }

        public void CancelStageList()
        {
            var cts = _cts;
            if (cts == null) return;

            _cts = null;
            cts.Cancel();
            cts.Dispose();
        }

        public void ShowStageInfo(StageDefinition stage, int? bestScore)
        {
            // ViewModel作成
            var viewModel = new ShowStageInfoViewModel(stage.Id.Id
                , stage.StageHour
                , stage.TimeLimitSeconds
                , stage.RequiredDeliveryCount
                , stage.TargetIds.Count
                , bestScore
                , bestScore.HasValue ? stage.ScoreRules.RankRule.Evaluate(bestScore.Value) : (ScoreRank?)null);

            _stageSelectView.ShowStageInfo(viewModel);
        }
    }
}
