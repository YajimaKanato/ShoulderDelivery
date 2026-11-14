using ShoulderDelivery.Adapter;
using ShoulderDelivery.Entity;
using ShoulderDelivery.Infrastructure;
using ShoulderDelivery.UseCase;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace ShoulderDelivery.CompositionRoot
{
    public class IngameCompositionRoot : MonoBehaviour
    {
        [Header("Master")]
        [SerializeField] StageDefinitionAsset _defaultStage;
        [SerializeField] TargetView[] _targets;

        [Header("Views")]
        [SerializeField] DeliveryTargetIndicator _targetIndicator;
        [SerializeField] InGameCountDownView _inGameCountDownView;
        [SerializeField] InGameHudView _inGameHudView;
        [SerializeField] GameResultView _gameResultView;
        [SerializeField] CardboardContextView _cardboardContextView;
        [SerializeField] DeliveryContextView _deliveryView;
        [SerializeField] CardboardLauncher _cardboardLauncher;
        [SerializeField] PlayerCardboardLaunch _playerCardboardLaunch;
        [SerializeField] CursorManager _cursorManager;
        [SerializeField] FadeDirector _director;
        [SerializeField] CitizenManager _citizenManager;

        GameUseCase _runningGame;
        bool _isStarted;

        private async void Start()
        {
            _targetIndicator.Initialize(_targets);
            var controller = _playerCardboardLaunch.GetComponent<PlayerController>();
            var presenter = new GamePresenter(_inGameHudView
                , _inGameCountDownView
                , _inGameHudView
                , _gameResultView
                , _deliveryView
                , _cardboardContextView
                , _targetIndicator
                , controller
                , _cursorManager
                , _deliveryView);

            var targetRepository = new TargetRepository(_targets.Select(target => target.BindDefinition()).ToArray());
            var references = new UseCaseReferences(new CardboardIdGenerator()
                , _cardboardLauncher
                , _cardboardLauncher
                , presenter
                , new GameSessionStore()
                , GameCompositionRoot.StageRepository
                , targetRepository);

            var game = new GameUseCase(references);
            var cardboard = new CardboardUseCase(references);
            _playerCardboardLaunch.OnThrow += cardboard.Throw;
            _cardboardLauncher.HitTarget += cardboard.HitTarget;
            _cardboardLauncher.CardboardExpired += cardboard.Discard;

            var stageId = new StageId(StageManager.GetStageId() ?? _defaultStage.StageId);
            game.Start(new GameStartInput(stageId));
            _runningGame = game;
            if (_director != null) await _director.FadeIn();
            if (this == null) return;
            _isStarted = true;
            if (_citizenManager != null)
                _citizenManager.OnHitPlayer += _runningGame.HitCitizen;
        }

        private void OnDestroy()
        {
            if (_citizenManager != null && _runningGame != null)
                _citizenManager.OnHitPlayer -= _runningGame.HitCitizen;
        }

        private async void Update()
        {
            if (!_isStarted) return;
            if (Time.deltaTime > 0)
                _runningGame?.Execute(new TickInput(Time.deltaTime));
        }
    }
}
