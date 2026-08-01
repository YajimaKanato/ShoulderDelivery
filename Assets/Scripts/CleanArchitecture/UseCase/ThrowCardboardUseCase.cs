using System;

namespace ShoulderDelivery.UseCase
{
    public sealed class ThrowCardboardUseCase
    {
        readonly IGameSessionStore _gameSessionStore;
        readonly ICardboardIdGenerator _cardboardIdGenerator;
        readonly ICardboardLauncher _launcher;
        readonly IGameOutputPort _outputPort;

        public ThrowCardboardUseCase(IGameSessionStore gameSessionStore
            , ICardboardIdGenerator cardboardIdGenerator
            , ICardboardLauncher launcher
            , IGameOutputPort outputPort)
        {
            if (gameSessionStore == null)
                throw new ArgumentNullException(nameof(gameSessionStore));

            if (cardboardIdGenerator == null)
                throw new ArgumentNullException(nameof(cardboardIdGenerator));

            if (launcher == null)
                throw new ArgumentNullException(nameof(launcher));

            if (outputPort == null)
                throw new ArgumentNullException(nameof(outputPort));

            _gameSessionStore = gameSessionStore;
            _cardboardIdGenerator = cardboardIdGenerator;
            _launcher = launcher;
            _outputPort = outputPort;
        }

        /// <summary>
        /// 段ボールの投擲をするメソッド
        /// </summary>
        /// <param name="input">投擲時の情報</param>
        /// <exception cref="InvalidOperationException">必要な参照がない</exception>
        public void Execute(ThrowCardboardInput input)
        {
            var session = _gameSessionStore.CurrentGameSession;
            if (session == null)
                throw new InvalidOperationException(nameof(session));

            var stageState = session.StageState;
            if (stageState == null)
                throw new InvalidOperationException(nameof(stageState));

            // ゲームをプレイ中でなければ無視
            if (!session.StageState.IsPlaying)
            {
                // 投擲失敗を通知
                _outputPort.ThrowCardboard(ThrowCardboardOutputService.Rejected());
                return;
            }

            // TODO : 他に投擲失敗を通知する場合は処理を追加

            var inFlightCardboardState = session.InFlightCardboardState;
            if (inFlightCardboardState == null)
                throw new InvalidOperationException(nameof(inFlightCardboardState));

            // ID生成
            var cardboardId = _cardboardIdGenerator.GenerateId();

            // 段ボールのIDと投擲時の情報を保存
            inFlightCardboardState.RegisterThrowContext(cardboardId, input.Context);

            // 投擲命令
            _launcher.LaunchCardboard(cardboardId, input.Context);

            // 結果を通知
            _outputPort.ThrowCardboard(ThrowCardboardOutputService.Accepted(cardboardId));
        }
    }
}
