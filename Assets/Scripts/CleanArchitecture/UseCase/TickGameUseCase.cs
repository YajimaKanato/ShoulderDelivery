using ShoulderDelivery.Entity;
using System;

namespace ShoulderDelivery.UseCase
{
    public class TickGameUseCase
    {
        readonly IGameSessionStore _gameSession;
        readonly IGameOutputPort _outputPort;

        public TickGameUseCase(IGameSessionStore gameSessionStore
            , IGameOutputPort outputPort)
        {
            if (gameSessionStore == null)
                throw new ArgumentNullException(nameof(gameSessionStore));

            if (outputPort == null)
                throw new ArgumentNullException(nameof(outputPort));

            _gameSession = gameSessionStore;
            _outputPort = outputPort;
        }

        public void Execute(TickInput input)
        {
            var session = _gameSession.CurrentGameSession;
            if (session == null)
                throw new InvalidOperationException(nameof(session));

            var stageState = session.StageState;
            if (stageState == null)
                throw new InvalidOperationException(nameof(stageState));

            // 現在の時間の管理状況によって処理を変える
            var result = stageState.CurrentPhase switch
            {
                StagePhase.CountDown => stageState.CountDown(input.Delta),
                StagePhase.IsPlaying => stageState.Tick(input.Delta),
                _ => StageTickResult.None
            };


        }
    }
}
