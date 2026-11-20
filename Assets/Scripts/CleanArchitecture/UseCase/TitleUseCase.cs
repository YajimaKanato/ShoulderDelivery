using System;
using System.Linq;
using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    public class TitleUseCase
    {
        readonly IStageRepository _stageRepository;
        readonly ITitleOutputPort _outputPort;
        readonly IStageClearStore _stageClearStore;

        /// <summary>現在選択しているステージの定義</summary>
        public StageDefinition SelectedStage { get; private set; }

        public TitleUseCase(IStageRepository stageRepository, ITitleOutputPort outputPort, IStageClearStore stageClearStore)
        {
            _stageRepository = stageRepository
                ?? throw new ArgumentNullException(nameof(stageRepository));
            _stageClearStore = stageClearStore
                ?? throw new ArgumentNullException(nameof(stageClearStore));
            _outputPort = outputPort
                ?? throw new ArgumentNullException(nameof(outputPort));
        }

        /// <summary>選択できるステージの一覧を表示するメソッド</summary>
        public void ShowStageList()
        {
            var stages = _stageRepository.GetStageIds()
                .Select(id => (id, _stageClearStore.IsCleared(id)))
                .ToArray();
            _outputPort.ShowStageList(stages);
        }

        /// <summary>ステージを選択して詳細を表示するメソッド</summary>
        /// <param name="stageId">選択したステージID</param>
        public void SelectStage(StageId stageId)
        {
            SelectedStage = _stageRepository.Get(stageId);
            _outputPort.ShowStageInfo(SelectedStage, _stageClearStore.GetBestScore(stageId));
        }

        /// <summary>選択中のステージを開始するメソッド</summary>
        /// <returns>開始するステージID</returns>
        public StageId? StartStage()
        {
            if (SelectedStage == null) return null;

            _outputPort.CancelStageList();
            return SelectedStage.Id;
        }
    }
}
