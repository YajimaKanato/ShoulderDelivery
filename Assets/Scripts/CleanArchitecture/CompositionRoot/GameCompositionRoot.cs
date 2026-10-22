using ShoulderDelivery.Adapter;
using ShoulderDelivery.Infrastructure;
using ShoulderDelivery.UseCase;
using UnityEngine;

namespace ShoulderDelivery.CompositionRoot
{
    public class GameCompositionRoot : MonoBehaviour
    {
        [Header("Master")]
        [SerializeField] StageDefinitionAsset[] _stages;
        [SerializeField] TargetDefinitionAsset[] _targets;

        [Header("Views")]
        [SerializeField] InGameCountDownView _inGameCountDownView;
        [SerializeField] InGameHudView _inGameHudView;
        [SerializeField] GameResultView _gameResultView;
        [SerializeField] CardboardContextView _cardboardContextView;
        [SerializeField] DeliveryView _deliveryView;
        [SerializeField] CardboardLauncher _cardboardLauncher;

        GamePresenter _presenter;
        UseCaseReferences _references;

        #region UseCase
        GameUseCase _gameUseCase;
        CardboardUseCase _cardboardUseCase;
        #endregion

        #region Repository
        StageRepository _stageRepository;
        TargetRepository _targetRepository;
        #endregion

        GameSessionStore _gameSessionStore;
        CardboardIdGenerator _cardboardIdGenerator;

        private void Awake()
        {
            _presenter = new(_inGameHudView
                , _inGameCountDownView
                , _inGameHudView
                , _gameResultView
                , _deliveryView
                , _cardboardContextView);

            _stageRepository = new(_stages);
            _targetRepository = new(_targets);
            _gameSessionStore = new();
            _cardboardIdGenerator = new();

            _references = new(_cardboardIdGenerator
                , _cardboardLauncher
                , _cardboardLauncher
                , _presenter
                , _gameSessionStore
                , _stageRepository
                , _targetRepository);

            _gameUseCase = new(_references);
            _cardboardUseCase = new(_references);
        }
    }
}
