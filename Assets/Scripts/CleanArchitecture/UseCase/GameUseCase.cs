using ShoulderDelivery.Entity;
using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの進行を管理するUseCaseクラス</summary>
    public sealed class GameUseCase
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
        public void Start(StartGameInput input)
        {
            // ステージの情報を取得
            var stageDefinition = _stageRepository.Get(input.StageId);

            // ゲームの情報を生成して登録
            var gameSession = new GameSession(stageDefinition);
            _gameSessionStore.Set(gameSession);

            // ゲーム開始を通知
            _outputPort.ShowHud(
                new GameHudOutput(
                     remainingTime: gameSession.StageState.RemainingTime,
                     remainingDeliveryCount: gameSession.DeliveryState.RemainigDeliveryCount,
                     score: gameSession.Score.Total
                ));
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
                StagePhase.CountDown => stageState.CountDown(input.Delta),
                StagePhase.IsPlaying => stageState.Tick(input.Delta),
                _ => StageTickResult.None
            };

            // 時間の進行結果に応じて処理を変える
            switch (result)
            {
                case StageTickResult.CountDownFinished:
                    // コントローラーを有効にする
                    _outputPort.ChangeControllerEnable(true);
                    break;
                case StageTickResult.TimeUp:
                    // ゲームを終了する
                    Finish();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ゲームを終了するメソッド
        /// </summary>
        /// <exception cref="InvalidOperationException">必要な参照がない</exception>
        public void Finish()
        {
            var session = _gameSessionStore.CurrentGameSession;
            if (session == null)
                throw new InvalidOperationException(nameof(session));

            var stageState = session.StageState;
            if (stageState == null)
                throw new InvalidOperationException(nameof(stageState));

            // ゲームを終了状態にする
            stageState.Finish();

            // ゲームの結果を送信する
            _outputPort.ShowResult(
                        new GameResultOutput(
                        total: session.Score.Total,
                        deliveryCount: session.DeliveryState.DeliveredCount
                        ));

            // 現在進行中のゲーム情報を削除する
            _gameSessionStore.Clear();
        }
    }
}
