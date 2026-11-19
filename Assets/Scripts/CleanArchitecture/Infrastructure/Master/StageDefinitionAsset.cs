using ShoulderDelivery.Common;
using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    [CreateAssetMenu(fileName = "Stage", menuName = "Master/StageDefinitionAsset")]
    public class StageDefinitionAsset : MasterAssetBase<StageDefinition>
    {
        [SerializeField, Tooltip("このステージのID")] string _stageId = "Stage";
        [SerializeField, Tooltip("ステージの時間帯")] StageHour _stageHour;
        [SerializeField, Tooltip("カウントダウンの時間")] int _countDownSeconds = 3;
        [SerializeField, Tooltip("制限時間")] int _timeLimitSeconds = 60;
        [SerializeField, Tooltip("配達目的地の情報アセット群")] TargetDefinitions _targetDefinitionAssets;
        [SerializeField, Tooltip("スコアのルールアセット")] ScoreRulesAsset _scoreRulesAsset;

        public string StageId => _stageId;

        public override StageDefinition ToEntity()
        {
            var stageId = new StageId(_stageId);
            var scoreRule = _scoreRulesAsset.ToEntity();

            // TargetIdの配列作成
            var targetIds = GenerateTargetIds();

            var timeLimit = _scoreRulesAsset.RemainingTimeBonusRuleAsset?.TimeLimit ?? _timeLimitSeconds;

            return new StageDefinition(stageId
                , _stageHour
                , _countDownSeconds
                , timeLimit
                , targetIds.Length
                , targetIds
                , scoreRule);
        }

        TargetId[] GenerateTargetIds()
        {
            var targetIndices = _scoreRulesAsset.DeliveryComboBonusRuleAsset.TargetIndices;

            var targets = _targetDefinitionAssets.Definitions;
            var targetIds = new TargetId[targetIndices.Count];

            for (int i = 0; i < targetIndices.Count; i++)
            {
                var index = Mathf.Clamp(targetIndices[i], 0, targets.Count - 1);
                targetIds[i] = targets[index].ToEntity().Id;
            }

            return targetIds;
        }
    }
}
