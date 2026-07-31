using ShoulderDelivery.Entity;
using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲーム開始を管理するUseCaseクラス</summary>
    public class StartGameUseCase
    {
        readonly IStageRepository _stageRepository;
        readonly IGameSessionStore _gameSessionStore;
        readonly IGameOutputPort _outputPort;

        public StartGameUseCase(IStageRepository stageRepository
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
        /// <exception cref="ArgumentNullException">データが不正である</exception>
        public void Execute(StartGameInput? input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // ステージの情報を取得
            var inputValue = input.Value;
            var stageDefinition = _stageRepository.Get(inputValue.StageId);

            // ゲームの情報を生成して登録
            var gameSession = new GameSession(stageDefinition);
            _gameSessionStore.Set(gameSession);

            // ゲーム開始を通知
            _outputPort.ShowHud(
                new GameHudOutput(
                    gameSession.StageState.RemainingTime
                    , gameSession.DeliveryState.RemainigDeliveryCount
                    , gameSession.Score.Total
                ));
        }
    }
}
