using ShoulderDelivery.Entity;
using System.Collections.Generic;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ステージ情報を保管する機能を持つインターフェース</summary>
    public interface IStageRepository : IRepository<StageDefinition, StageId>
    {
        /// <summary>登録されているステージIDを一覧で取得するメソッド</summary>
        IReadOnlyList<StageId> GetStageIds();
    }
}
