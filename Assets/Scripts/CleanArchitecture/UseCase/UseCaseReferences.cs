using System;
using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>UseCaseで利用する参照群</summary>
    public sealed class UseCaseReferences
    {
        public readonly ICardboardIdGenerator CardboardIdGenerator;
        public readonly ICardboardLauncher Launcher;
        public readonly IDiscardCardboard DiscardCardboard;
        public readonly IGameOutputPort OutputPort;
        public readonly IGameSessionStore GameSessionStore;
        public readonly IStageRepository StageRepository;
        public readonly ITargetRepository TargetRepository;

        public GameSession CurrentGameSession
            => GameSessionStore?.CurrentGameSession
            ?? throw new InvalidOperationException();

        public StageState StageState
            => CurrentGameSession?.StageState
            ?? throw new InvalidOperationException();

        public Score Score
            => CurrentGameSession?.Score
            ?? throw new InvalidOperationException();

        public StageDefinition StageDefinition
            => CurrentGameSession?.StageDefinition
            ?? throw new InvalidOperationException();

        public DeliveryState DeliveryState
            => CurrentGameSession?.DeliveryState
            ?? throw new InvalidOperationException();

        public InFlightCardboardState InFlightCardboardState
            => CurrentGameSession?.InFlightCardboardState
            ?? throw new InvalidOperationException();

        public ScoreRules ScoreRules
            => StageDefinition?.ScoreRules
            ?? throw new InvalidOperationException();

        public UseCaseReferences(ICardboardIdGenerator cardboardIdGenerator
            , ICardboardLauncher launcher
            , IDiscardCardboard discardCardboard
            , IGameOutputPort outputPort
            , IGameSessionStore gameSessionStore
            , IStageRepository stageRepository
            , ITargetRepository targetRepository)
        {
            CardboardIdGenerator = cardboardIdGenerator
                ?? throw new ArgumentNullException(nameof(cardboardIdGenerator));
            Launcher = launcher
                ?? throw new ArgumentNullException(nameof(launcher));
            DiscardCardboard = discardCardboard
                ?? throw new ArgumentNullException(nameof(discardCardboard));
            OutputPort = outputPort
                ?? throw new ArgumentNullException(nameof(outputPort));
            GameSessionStore = gameSessionStore
                ?? throw new ArgumentNullException(nameof(gameSessionStore));
            StageRepository = stageRepository
                ?? throw new ArgumentNullException(nameof(stageRepository));
            TargetRepository = targetRepository
                ?? throw new ArgumentNullException(nameof(targetRepository));
        }
    }
}
