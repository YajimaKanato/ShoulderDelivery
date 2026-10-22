using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールを削除する機能を持つインターフェース</summary>
    public interface IDiscardCardboard
    {
        /// <summary>段ボールを削除するメソッド</summary>
        /// <param name="cardboardId">削除する段ボールのID</param>
        void DiscardCardboard(CardboardId cardboardId);
    }
}
