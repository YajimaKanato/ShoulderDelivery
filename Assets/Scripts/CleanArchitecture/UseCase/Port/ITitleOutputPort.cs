using ShoulderDelivery.Entity;
using System.Collections.Generic;

namespace ShoulderDelivery.UseCase
{
    /// <summary>タイトルの状態を出力する機能を持つインターフェース</summary>
    public interface ITitleOutputPort
    {
        /// <summary>選択できるステージの一覧を出力するメソッド</summary>
        /// <param name="stages">ステージIDとクリア状況</param>
        void ShowStageList(IReadOnlyList<(StageId Id, bool IsCleared)> stages);

        /// <summary>選択したステージの情報を出力するメソッド</summary>
        /// <param name="stage">選択したステージの定義</param>
        void ShowStageInfo(StageDefinition stage);

        /// <summary>ステージ選択ボタンの生成を中断するメソッド</summary>
        void CancelStageList();
    }
}
