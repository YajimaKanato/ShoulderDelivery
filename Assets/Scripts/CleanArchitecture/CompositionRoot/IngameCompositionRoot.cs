using Cysharp.Threading.Tasks;
using ShoulderDelivery.Adapter;
using ShoulderDelivery.Entity;
using ShoulderDelivery.Infrastructure;
using ShoulderDelivery.UseCase;
using System;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.CompositionRoot
{
    public class IngameCompositionRoot : MonoBehaviour
    {
        [SerializeField] float _waitTime = 0.5f;
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
        bool _loadingScene;
        PlayerController _controller;

        private async void Start()
        {
            _targetIndicator.Initialize(_targets);
            var controller = _playerCardboardLaunch.GetComponent<PlayerController>();
            _controller = controller;
            _inGameHudView?.SetSpeedSource(_playerCardboardLaunch.GetComponent<Rigidbody>());
            _gameResultView?.gameObject.SetActive(false);
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

            var game = new GameUseCase(references, GameCompositionRoot.StageClearStore);
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

        /// <summary>リザルト画面からタイトルに戻るメソッド</summary>
        public void BackTitle()
        {
            ReturnTitle(false).Forget();
        }

        /// <summary>リザルト画面からステージ選択に戻るメソッド</summary>
        public void BackStageSelect()
        {
            ReturnTitle(true).Forget();
        }

        async UniTask ReturnTitle(bool openStageSelect)
        {
            if (_loadingScene) return;
            _loadingScene = true;

            await FadeOutAsync();
            if (this == null) return;
            await StageManager.LoadTitle(openStageSelect);
        }

        async UniTask FadeOutAsync()
        {
            if (_director != null) await _director.FadeOut();
            await UniTask.Delay(TimeSpan.FromSeconds(_waitTime));
        }

        public void RetryStage()
        {
            if (_loadingScene) return;
            _loadingScene = true;
            _controller.SetControllerEnabled(false);
            RetryStageAsync().Forget();
        }

        async UniTask RetryStageAsync()
        {
            // 開始時のフェードと同じDirectorを使うので、先に再生が終わるのを待つ
            await UniTask.WaitUntil(() => _isStarted, cancellationToken: this.GetCancellationTokenOnDestroy());
            await FadeOutAsync();
            if (this == null) return;
            await StageManager.LoadStage(StageManager.GetStageId() ?? _defaultStage.StageId);
        }

        private void OnDestroy()
        {
            if (_citizenManager != null && _runningGame != null)
                _citizenManager.OnHitPlayer -= _runningGame.HitCitizen;
        }

        private void Update()
        {
            if (_loadingScene) return;
            if (_isStarted && _controller != null && _controller.QuitPressed)
            {
                _controller.SetControllerEnabled(false);
                BackStageSelect();
                return;
            }

            if (_controller != null && _controller.RestartPressed)
            {
                RetryStage();
                return;
            }
            if (!_isStarted) return;
            if (Time.deltaTime > 0)
                _runningGame?.Execute(new TickInput(Time.deltaTime));
        }
    }
}
