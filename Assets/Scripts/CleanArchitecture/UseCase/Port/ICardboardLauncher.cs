using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールを飛ばす機能を持つインターフェース</summary>
    public interface ICardboardLauncher
    {
        /// <summary>段ボールを飛ばすメソッド</summary>
        /// <param name="id">飛ばす段ボールのID</param>
        /// <param name="context">投擲時の情報</param>
        void LaunchCardboard(CardboardId id, ThrowContext context);
    }
}
