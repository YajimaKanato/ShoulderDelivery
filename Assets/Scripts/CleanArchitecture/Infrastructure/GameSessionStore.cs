using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;
using System;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>ゲームの情報を持つクラス</summary>
    public class GameSessionStore : IGameSessionStore
    {
        GameSession _currentGameSession;

        public GameSession CurrentGameSession => _currentGameSession;

        public void Clear()
        {
            _currentGameSession = null;
        }

        public void Set(GameSession session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            _currentGameSession = session;
        }
    }
}
