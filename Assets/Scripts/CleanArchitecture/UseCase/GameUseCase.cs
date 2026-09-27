using ShoulderDelivery.Entity;
using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの進行を管理するUseCaseクラス</summary>
    public sealed class GameUseCase : IGameFinishable
    {
        readonly IStageRepository _stageRepository;
        readonly IGameSessionStore _gameSessionStore;
        readonly IGameOutputPort _outputPort;

        public GameUseCase(IStageRepository stageRepository
            , IGameSessionStore gameSessionStore
            , IGameOutputPort outputPort)
        {
            if (stageRepository == null)
                throw new ArgumentNullException(nameof(stageRepository));

            if (gameSessionStore == null)
                throw new ArgumentNullException(nameof(gameSessionStore));

            if (outputPort == null)
                throw new ArgumentNullException(nameof(outputPort));

            _stageRepository = stageRepository;
            _gameSessionStore = gameSessionStore;
            _outputPort = outputPort;
        }

        /// <summary>
        /// ゲームを開始するメソッド
        /// </summary>
        /// <param name="input">開始に必要なデータ</param>
        public void Start(GameStartInput input)
        {
            // ステージの情報を取得
            var stageDefinition = _stageRepository.Get(input.StageId);

            if (stageDefinition == null)
                throw new InvalidOperationException(nameof(stageDefinition));

            // ゲームの情報を生成して登録
            var gameSession = new GameSession(stageDefinition);
            _gameSessionStore.Set(gameSession);

            // ゲーム開始を通知
            _outputPort.ShowStageInfo(
                new StageInfoOutput(stageDefinition.TimeLimitSeconds
                , stageDefinition.RequiredDeliveryCount));
        }

        /// <summary>
        /// ゲームを毎フレーム更新するメソッド
        /// </summary>
        /// <param name="input">更新に必要なデータ</param>
        /// <exception cref="InvalidOperationException">必要な参照がない</exception>
        public void Execute(TickInput input)
        {
            var session = _gameSessionStore.CurrentGameSession;
            if (session == null)
                throw new InvalidOperationException(nameof(session));

            var stageState = session.StageState;
            if (stageState == null)
                throw new InvalidOperationException(nameof(stageState));

            // 現在の時間の進行状況によって処理を変える
            var result = stageState.CurrentPhase switch
            {
                StagePhase.CountDown => CountDown(input, stageState),
                StagePhase.IsPlaying => Tick(input, session, stageState),
                _ => StageTickResult.None
            };

            // 時間の進行結果に応じて処理を変える
            switch (result)
            {
                case StageTickResult.CountDownFinished:
                    CountDownFinished();
                    break;
                case StageTickResult.TimeUp:
                    // ゲームを終了する
                    FinishGame();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// カウントダウン情報を更新するメソッド
        /// </summary>
        /// <param name="input">チック情報</param>
        /// <param name="stageState">ステージ情報</param>
        /// <returns>ステージのチック結果</returns>
        StageTickResult CountDown(TickInput input, StageState stageState)
        {
            // カウントダウンを進める
            var result = stageState.CountDown(input.Delta);

            // カウントダウンを表示
            _outputPort.ShowCountDown(new CountDownOutput(stageState.RemainingCountDownSeconds));

            return result;
        }

        /// <summary>
        /// 毎フレーム情報を更新するメソッド
        /// </summary>
        /// <param name="session">ゲームのセッション情報</param>
        /// <param name="stageState">ステージの情報</param>
        /// <exception cref="InvalidOperationException">必要な参照がない</exception>
        /// <returns>ステージのチック結果</returns>
        StageTickResult Tick(TickInput input, GameSession session, StageState stageState)
        {
            var score = session.Score;
            if (score == null)
                throw new InvalidOperationException(nameof(score));

            var result = stageState.Tick(input.Delta);

            // 情報更新
            _outputPort.ShowHud(new GameHudOutput(stageState.RemainingTime, score.Total));

            return result;
        }

        /// <summary>
        /// カウントダウン終了時のイベントメソッド
        /// </summary>
        void CountDownFinished()
        {
            _outputPort.ShowGameStart(new GameStartOutput());
            _outputPort.ChangeControllerEnable(true);
        }

        /// <summary>
        /// ゲームを終了するメソッド
        /// </summary>
        /// <exception cref="InvalidOperationException">必要な参照がない</exception>
        public void FinishGame()
        {
            var gameSession = _gameSessionStore.CurrentGameSession;
            if (gameSession == null)
                throw new InvalidOperationException(nameof(gameSession));

            var stageState = gameSession.StageState;
            if (stageState == null)
                throw new InvalidOperationException(nameof(stageState));

            // ゲームを終了状態にする
            if (!stageState.Finish()) return;

            var stageDefinition = gameSession.StageDefinition;
            if (stageDefinition == null)
                throw new InvalidOperationException(nameof(stageDefinition));

            var scoreRules = stageDefinition.ScoreRules;
            if (scoreRules == null)
                throw new InvalidOperationException(nameof(scoreRules));

            // 残り時間ボーナスを計算
            var timeBonus = ScoreCalculator.CalculateRemainingSecondsScore(stageState, scoreRules);

            var score = gameSession.Score;
            if (score == null)
                throw new InvalidOperationException(nameof(score));

            // スコアを更新
            score.AddScore(timeBonus);

            var deliveryState = gameSession.DeliveryState;
            if (deliveryState == null)
                throw new InvalidOperationException(nameof(deliveryState));

            // 結果を表示
            if (deliveryState.IsQuotaMet)
            {
                _outputPort.ShowGameClear(new GameClearOutput(score.Total
                    , deliveryState.RequiredDeliveryCount
                    , deliveryState.DeliveredCount
                    , stageDefinition.TimeLimitSeconds
                    , stageState.RemainingTime));
            }
            else
            {
                _outputPort.ShowGameFailed(new GameFailedOutput(score.Total
                    , deliveryState.RequiredDeliveryCount
                    , deliveryState.DeliveredCount
                    , stageDefinition.TimeLimitSeconds
                    , stageState.RemainingTime));
            }
        }
    }
}
