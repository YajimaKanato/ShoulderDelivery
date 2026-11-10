using ShoulderDelivery.Entity;
using System.Collections.Generic;

namespace ShoulderDelivery.UseCase
{
    /// <summary>タイトルの状態を出力する機能を持つインターフェース</summary>
    public interface ITitleOutputPort
    {
        /// <summary>選択できるステージの一覧を出力するメソッド</summary>
        /// <param name="stageIds">登録されているステージID</param>
        void ShowStageList(IReadOnlyList<StageId> stageIds);

        /// <summary>選択したステージの情報を出力するメソッド</summary>
        /// <param name="stage">選択したステージの定義</param>
        void ShowStageInfo(StageDefinition stage);
    }
}
