using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>配達に関する流れを持つインターフェース</summary>
    public interface IDeliveryUseCase
    {
        /// <summary>配達を検証するメソッド</summary>
        /// <param name="targetId">配達対象</param>
        /// <param name="context">配達時の情報</param>
        /// <returns>配達できたか</returns>
        bool TryDelivery(TargetId? targetId, ThrowContext context);
    }
}
