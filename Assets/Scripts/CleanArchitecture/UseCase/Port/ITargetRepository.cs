using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ターゲットの情報を保管する機能を持つインターフェース</summary>
    public interface ITargetRepository : IRepository<TargetDefinition, TargetId>
    {

    }
}
