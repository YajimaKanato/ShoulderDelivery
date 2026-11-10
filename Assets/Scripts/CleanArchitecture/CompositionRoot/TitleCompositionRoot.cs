using ShoulderDelivery.Adapter;
using ShoulderDelivery.Infrastructure;
using ShoulderDelivery.UseCase;
using UnityEngine;

namespace ShoulderDelivery.CompositionRoot
{
    public class TitleCompositionRoot : MonoBehaviour
    {
        [SerializeField] StageSelectView _stageSelectView;
        TitleUseCase _titleUseCase;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            var presenter = new TitlePresenter(_stageSelectView);
            _titleUseCase = new TitleUseCase(GameCompositionRoot.StageRepository, presenter);
            _stageSelectView.Initialize();
            _titleUseCase.ShowStageList();
            _stageSelectView.OnStageSelected += _titleUseCase.SelectStage;
            _stageSelectView.OnStart += StartStage;
        }

        private void Update()
        {
            if (_stageSelectView.HasTitleInput())
                _stageSelectView.OpenStageSelect();
        }

        /// <summary>選択中のステージを開始するメソッド</summary>
        void StartStage()
        {
            var stage = _titleUseCase.SelectedStage;
            if (stage == null) return;

            _stageSelectView.ShowLoading();
            StageManager.LoadStage(stage.Id.Id);
        }

        private void OnDestroy()
        {
            if (!_stageSelectView || _titleUseCase == null) return;
            _stageSelectView.OnStageSelected -= _titleUseCase.SelectStage;
            _stageSelectView.OnStart -= StartStage;
        }
    }
}
