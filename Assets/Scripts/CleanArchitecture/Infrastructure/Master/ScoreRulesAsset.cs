using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    [CreateAssetMenu(fileName = "Score", menuName = "Master/ScoreRulesAsset")]
    public class ScoreRulesAsset : MasterAssetBase<ScoreRules>
    {
        [SerializeField, Tooltip("配達成功時の基礎スコア")] int _deliverySuccessScore;

        [SerializeField, Tooltip("連続配達成功回数に応じたボーナスのテーブルアセット")]
        DeliveryComboBonusRuleAsset _deliveryComboBonusRuleAsset;

        [SerializeField, Tooltip("投擲時の移動速度に応じたボーナスのテーブルアセット")]
        SpeedBonusRuleAsset _speedBonusRuleAsset;

        [SerializeField, Tooltip("投擲距離に応じたボーナスのテーブルアセット")]
        DistanceBonusRuleAsset _distanceBonusRuleAsset;

        [SerializeField, Tooltip("残り時間に応じたボーナスのテーブルアセット")]
        RemainingTimeBonusRuleAsset _remainingTimeBonusRuleAsset;

        public override ScoreRules ToEntity()
        {
            var deliveryComboBonusRule = _deliveryComboBonusRuleAsset.ToEntity();
            var speedBonusRule = _speedBonusRuleAsset.ToEntity();
            var distanceBonusRule = _distanceBonusRuleAsset.ToEntity();
            var remainingTimeBonusRule = _remainingTimeBonusRuleAsset.ToEntity();

            return new ScoreRules(_deliverySuccessScore
                , deliveryComboBonusRule
                , speedBonusRule
                , distanceBonusRule
                , remainingTimeBonusRule);
        }

#if UNITY_EDITOR
        public void SetDeliveryComboBonusRuleAsset(DeliveryComboBonusRuleAsset asset)
        {
            _deliveryComboBonusRuleAsset = asset;
        }

        public void SetSpeedBonusRuleAsset(SpeedBonusRuleAsset asset)
        {
            _speedBonusRuleAsset = asset;
        }

        public void SetDistanceBonusRuleAsset(DistanceBonusRuleAsset asset)
        {
            _distanceBonusRuleAsset = asset;
        }

        public void SetRemainingTimeBonusRuleAsset(RemainingTimeBonusRuleAsset asset)
        {
            _remainingTimeBonusRuleAsset = asset;
        }
#endif
    }
}
