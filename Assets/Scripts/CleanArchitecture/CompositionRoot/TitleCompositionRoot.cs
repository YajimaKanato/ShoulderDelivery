using Cysharp.Threading.Tasks;
using ShoulderDelivery.Adapter;
using ShoulderDelivery.Infrastructure;
using ShoulderDelivery.UseCase;
using System;
using UnityEngine;

namespace ShoulderDelivery.CompositionRoot
{
    public class TitleCompositionRoot : MonoBehaviour
    {
        [SerializeField] StageSelectView _stageSelectView;
        [SerializeField] TitleTimelineManager _timelineManager;
        [SerializeField] float _waitTime = 0.5f;
        TitleUseCase _titleUseCase;
        TitlePresenter _presenter;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _presenter = new TitlePresenter(_stageSelectView);
            _titleUseCase = new TitleUseCase(GameCompositionRoot.StageRepository, _presenter, GameCompositionRoot.StageClearStore);
            _stageSelectView.Initialize();
            _stageSelectView.OnStageSelected += _titleUseCase.SelectStage;
            _stageSelectView.OnStart += StartStage;
            _stageSelectView.OnOpenStageSelect += _titleUseCase.ShowStageList;
            if (StageManager.ConsumeOpenStageSelect())
                _timelineManager.StartFromStageSelect();
        }

        /// <summary>選択中のステージを開始するメソッド</summary>
        async void StartStage()
        {
            var stageId = _titleUseCase.StartStage();
            if (!stageId.HasValue) return;

            _stageSelectView.ShowLoading();
            await _stageSelectView.FadeOut();
            if (this == null) return;
            await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));
            if (this == null) return;
            await StageManager.LoadStage(stageId.Value.Id);
        }

        private void OnDestroy()
        {
            _presenter?.CancelStageList();
            if (!_stageSelectView || _titleUseCase == null) return;
            _stageSelectView.OnStageSelected -= _titleUseCase.SelectStage;
            _stageSelectView.OnStart -= StartStage;
            _stageSelectView.OnOpenStageSelect -= _titleUseCase.ShowStageList;
        }
    }
}
