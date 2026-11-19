using ShoulderDelivery.Entity;
using System.Collections.Generic;

namespace ShoulderDelivery.Adapter
{
    /// <summary>ステージ選択ボタンを作成するためのViewModel</summary>
    public readonly struct CreateStageSelectViewModel
    {
        public readonly IReadOnlyList<(StageId Id, bool IsCleared)> Stages;
        public int StageCount => Stages.Count;

        public CreateStageSelectViewModel(IReadOnlyList<(StageId Id, bool IsCleared)> stages)
        {
            Stages = stages;
        }
    }
}

