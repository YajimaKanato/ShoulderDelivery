using ShoulderDelivery.Adapter;
using ShoulderDelivery.Entity;
using ShoulderDelivery.Infrastructure;
using ShoulderDelivery.UseCase;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.CompositionRoot
{
    public class IngameCompositionRoot : MonoBehaviour
    {
        [Header("Master")]
        [SerializeField] StageDefinitionAsset _defaultStage;
        [SerializeField] TargetView[] _targets;
        [SerializeField] DeliveryTargetIndicator _targetIndicator;

        [Header("Views")]
        [SerializeField] InGameCountDownView _inGameCountDownView;
        [SerializeField] InGameHudView _inGameHudView;
        [SerializeField] GameResultView _gameResultView;
        [SerializeField] CardboardContextView _cardboardContextView;
        [SerializeField] DeliveryContextView _deliveryView;
        [SerializeField] CardboardLauncher _cardboardLauncher;
        [SerializeField] PlayerCardboardLaunch _playerCardboardLaunch;

        GameUseCase _runningGame;

        private void Start()
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
                , controller);

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
            _playerCardboardLaunch.SetCardboardUseCase(cardboard);
            _cardboardLauncher.HitTarget += cardboard.HitTarget;
            _cardboardLauncher.CardboardExpired += cardboard.Discard;

            var stageId = new StageId(GameCompositionRoot.GetStageId() ?? _defaultStage.StageId);
            game.Start(new GameStartInput(stageId));
            _runningGame = game;
        }

        private void Update()
        {
            if (Time.deltaTime > 0)
                _runningGame?.Execute(new TickInput(Time.deltaTime));
        }
    }
}
