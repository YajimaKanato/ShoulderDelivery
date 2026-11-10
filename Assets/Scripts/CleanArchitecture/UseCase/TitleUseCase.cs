using System;
using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    public class TitleUseCase
    {
        readonly IStageRepository _stageRepository;
        readonly ITitleOutputPort _outputPort;

        /// <summary>現在選択しているステージの定義</summary>
        public StageDefinition SelectedStage { get; private set; }

        public TitleUseCase(IStageRepository stageRepository, ITitleOutputPort outputPort)
        {
            _stageRepository = stageRepository
                ?? throw new ArgumentNullException(nameof(stageRepository));
            _outputPort = outputPort
                ?? throw new ArgumentNullException(nameof(outputPort));
        }

        /// <summary>選択できるステージの一覧を表示するメソッド</summary>
        public void ShowStageList()
        {
            _outputPort.ShowStageList(_stageRepository.GetStageIds());
        }

        /// <summary>ステージを選択して詳細を表示するメソッド</summary>
        /// <param name="stageId">選択したステージID</param>
        public void SelectStage(StageId stageId)
        {
            SelectedStage = _stageRepository.Get(stageId);
            _outputPort.ShowStageInfo(SelectedStage);
        }
    }
}
