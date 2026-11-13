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
        [SerializeField] float _waitTime = 0.5f;
        TitleUseCase _titleUseCase;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            var presenter = new TitlePresenter(_stageSelectView);
            _titleUseCase = new TitleUseCase(GameCompositionRoot.StageRepository, presenter);
            _stageSelectView.Initialize();
            _stageSelectView.OnStageSelected += _titleUseCase.SelectStage;
            _stageSelectView.OnStart += StartStage;
            _stageSelectView.OnOpenStageSelect += _titleUseCase.ShowStageList;
        }

        /// <summary>選択中のステージを開始するメソッド</summary>
        async void StartStage()
        {
            var stage = _titleUseCase.SelectedStage;
            if (stage == null) return;

            _stageSelectView.ShowLoading();
            await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));
            await StageManager.LoadStage(stage.Id.Id);
        }

        private void OnDestroy()
        {
            if (!_stageSelectView || _titleUseCase == null) return;
            _stageSelectView.OnStageSelected -= _titleUseCase.SelectStage;
            _stageSelectView.OnStart -= StartStage;
            _stageSelectView.OnOpenStageSelect -= _titleUseCase.ShowStageList;
        }
    }
}
