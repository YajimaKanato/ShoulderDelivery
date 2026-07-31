using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ステージ情報を保管する機能を持つインターフェース</summary>
    public interface IStageRepository
    {
        /// <summary>ステージ情報を返すメソッド</summary>
        /// <param name="stageId">ステージID</param>
        /// <returns>ステージ情報</returns>
        StageDefinition Get(StageId stageId);
    }
}
