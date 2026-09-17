using ShoulderDelivery.Entity;
using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールの結果を管理するUseCaseクラス</summary>
    public sealed class ResolveDeliveryUseCase
    {
        readonly IGameSessionStore _sessionStore;
        readonly IGameOutputPort _outputPort;
        readonly ITargetRepository _targetRepository;
        readonly IGameFinishable _gameUseCase;

        public ResolveDeliveryUseCase(IGameSessionStore sessionStore
            , IGameOutputPort outputPort
            , ITargetRepository targetRepository
            , IGameFinishable gameUseCase)
        {
            if (sessionStore == null)
                throw new ArgumentNullException(nameof(sessionStore));

            if (outputPort == null)
                throw new ArgumentNullException(nameof(outputPort));

            if (targetRepository == null)
                throw new ArgumentNullException(nameof(targetRepository));

            if (gameUseCase == null)
                throw new ArgumentNullException(nameof(gameUseCase));

            _sessionStore = sessionStore;
            _outputPort = outputPort;
            _targetRepository = targetRepository;
            _gameUseCase = gameUseCase;
        }

        /// <summary>
        /// 配達を処理するメソッド
        /// </summary>
        /// <param name="input">配達結果</param>
        /// <exception cref="InvalidOperationException">必要な参照がない</exception>
        public void Resolve(ResolveDeliveryInput input)
        {
            var session = _sessionStore.CurrentGameSession;
            if (session == null)
                throw new InvalidOperationException(nameof(session));

            var stageState = session.StageState;
            if (stageState == null)
                throw new InvalidOperationException(nameof(stageState));

            // ゲームをプレイ中でなければ無視
            if (!stageState.IsPlaying) return;

            var inFlightCardboardState = session.InFlightCardboardState;
            if (inFlightCardboardState == null)
                throw new InvalidOperationException(nameof(inFlightCardboardState));

            // 投擲時の情報を取得する
            if (!inFlightCardboardState.TryResolve(input.CardboardId, out var context)) return;

            var deliveryState = session.DeliveryState;
            if (deliveryState == null)
                throw new InvalidOperationException(nameof(deliveryState));

            // 配達をする
            if (!deliveryState.TryDelivery(input.TargetId))
            {
                // 配達失敗
                _outputPort.ShowDeliveryResult(DeliveryResultOutputService.Missed());
                return;
            }

            // ターゲットの情報を取得
            var targetDefinition = _targetRepository.Get(input.TargetId);

            // 配達成功情報を取得
            var deliveryResult = DeliveryResultService.Delivered(targetDefinition, deliveryState.DeliveryCombo);

            var scoreRules = session.StageDefinition?.ScoreRules;
            if (scoreRules == null)
                throw new InvalidOperationException(nameof(scoreRules));

            // スコアを生成
            var scoreBreakdown = ScoreCalculator.CalculateDeliveryScore(context, deliveryResult, scoreRules);

            var score = session.Score;
            if (score == null)
                throw new InvalidOperationException(nameof(score));

            // スコアを更新
            score.AddScore(scoreBreakdown);

            // 配達成功を通知
            _outputPort.ShowDeliveryResult(DeliveryResultOutputService.Delivered(scoreBreakdown, score.Total));

            if (deliveryState.IsQuataMet)
            {
                // ノルマ達成でゲーム終了
                _gameUseCase.FinishGame();
                return;
            }
        }
    }
}
