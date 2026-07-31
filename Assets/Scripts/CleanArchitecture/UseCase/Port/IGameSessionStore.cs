using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>現在のゲームの状況を管理する機能を持つインターフェース</summary>
    public interface IGameSessionStore
    {
        /// <summary>現在のゲームの情報</summary>
        GameSession CurrentGameSession { get; }

        /// <summary>ゲームの情報を登録するメソッド</summary>
        /// <param name="session">登録する情報</param>
        void Set(GameSession session);

        /// <summary>ゲームの情報を削除するメソッド</summary>
        void Clear();
    }
}
