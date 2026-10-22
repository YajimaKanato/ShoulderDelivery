using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ステージ情報を保管する機能を持つインターフェース</summary>
    public interface IStageRepository : IRepository<StageDefinition, StageId>
    {

    }
}
