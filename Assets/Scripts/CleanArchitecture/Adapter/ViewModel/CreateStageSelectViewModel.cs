using ShoulderDelivery.Entity;
using System.Collections.Generic;

namespace ShoulderDelivery.Adapter
{
    /// <summary>ステージ選択ボタンを作成するためのViewModel</summary>
    public readonly struct CreateStageSelectViewModel
    {
        public readonly IReadOnlyList<StageId> StageIds;
        public int StageCount => StageIds.Count;

        public CreateStageSelectViewModel(IReadOnlyList<StageId> stageIds)
        {
            StageIds = stageIds;
        }
    }
}

