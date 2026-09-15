using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    [CreateAssetMenu(fileName = "DistanceBonusRuleAsset", menuName = "Master/DistanceBonusRuleAsset")]
    public class DistanceBonusRuleAsset : MasterAssetBase<DistanceBonusRule>
    {
        [SerializeField, Tooltip("投擲距離に応じたボーナスのテーブル")] DistanceBonus[] _bonusTable;

        public override DistanceBonusRule ToEntity()
        {
            return new DistanceBonusRule(_bonusTable);
        }
    }
}
