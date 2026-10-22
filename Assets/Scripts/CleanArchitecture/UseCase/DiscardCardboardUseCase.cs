using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールが消えたことを管理するUseCaseクラス</summary>
    public class DiscardCardboardUseCase
    {
        readonly IGameSessionStore _gameSessionStore;
        readonly IDiscardCardboard _discardCardboard;

        public DiscardCardboardUseCase(IGameSessionStore gameSessionStore
            , IDiscardCardboard discardCardboard)
        {
            if (gameSessionStore == null)
                throw new ArgumentNullException(nameof(gameSessionStore));

            if (discardCardboard == null)
                throw new ArgumentNullException(nameof(discardCardboard));

            _gameSessionStore = gameSessionStore;
            _discardCardboard = discardCardboard;
        }

        /// <summary>
        /// 段ボールが消えたことを処理するメソッド
        /// </summary>
        /// <param name="input">消えた段ボールの情報</param>
        /// <exception cref="InvalidOperationException">必要な参照がない</exception>
        public void Discard(DiscardCardboardInput input)
        {
            var session = _gameSessionStore.CurrentGameSession;
            if (session == null)
                throw new InvalidOperationException(nameof(session));

            var inFlightCardboardState = session.InFlightCardboardState;
            if (inFlightCardboardState == null)
                throw new InvalidOperationException(nameof(inFlightCardboardState));

            // 段ボールを消す
            var id = input.Id;
            inFlightCardboardState.Discard(id);
            _discardCardboard.DiscardCardboard(id);
        }
    }
}
