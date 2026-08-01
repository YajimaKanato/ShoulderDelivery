using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールのIDを生成する機能を持つインターフェース</summary>
    public interface ICardboardIdGenerator
    {
        /// <summary>段ボールのIDを生成するメソッド</summary>
        /// <returns>段ボールのID</returns>
        CardboardId GenerateId();
    }
}
