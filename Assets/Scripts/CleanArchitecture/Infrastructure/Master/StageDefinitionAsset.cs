using UnityEngine;
using ShoulderDelivery.Entity;
using System.Linq;

namespace ShoulderDelivery.Infrastructure
{
    [CreateAssetMenu(fileName = "Stage", menuName = "Master/StageDefinitionAsset")]
    public class StageDefinitionAsset : MasterAssetBase<StageDefinition>
    {
        [SerializeField, Tooltip("このステージのID")] string _stageId = "Stage";
        [SerializeField, Tooltip("カウントダウンの時間")] int _countDownSeconds = 3;
        [SerializeField, Tooltip("制限時間")] int _timeLimitSeconds = 60;
        [SerializeField, Tooltip("必要な配達数")] int _requiredDeliveryCount = 3;
        [SerializeField, Tooltip("配達目的地の情報アセット群")] TargetDefinitionAsset[] _targetDefinitionAssets;
        [SerializeField, Tooltip("スコアのルールアセット")] ScoreRulesAsset _scoreRulesAsset;

        public string StageId => _stageId;

        public override StageDefinition ToEntity()
        {
            var stageId = new StageId(_stageId);
            var targetIds = new TargetId[_targetDefinitionAssets.Length];
            var scoreRule = _scoreRulesAsset.ToEntity();

            // TargetIdの配列作成
            targetIds = _targetDefinitionAssets.Select(asset => asset.ToEntity().Id).ToArray();

            return new StageDefinition(stageId
                , _countDownSeconds
                , _timeLimitSeconds
                , _requiredDeliveryCount
                , targetIds
                , scoreRule);
        }

        /// <summary>
        /// 配達目的地の情報アセットを設定するメソッド
        /// </summary>
        /// <param name="targetDefinitionAssets">配達目的地の情報アセット群</param>
        public void SetTargetDefinitions(TargetDefinitionAsset[] targetDefinitionAssets)
        {
            _targetDefinitionAssets = targetDefinitionAssets;
        }
    }
}
