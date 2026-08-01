using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ターゲットの情報を保管する機能を持つインターフェース</summary>
    public interface ITargetRepository
    {
        /// <summary>ターゲットの情報を返すメソッド</summary>
        /// <param name="targetId">ターゲットのID</param>
        /// <returns>ターゲットの情報</returns>
        TargetDefinition Get(TargetId targetId);
    }
}
