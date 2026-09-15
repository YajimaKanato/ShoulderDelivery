using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    [CreateAssetMenu(fileName = "RemainingTimeBonusRuleAsset", menuName = "Master/RemainingTimeBonusRuleAsset")]
    public class RemainingTimeBonusRuleAsset : MasterAssetBase<RemainingTimeBonusRule>
    {
        [SerializeField, Tooltip("残り時間に応じたボーナスのテーブル")] RemainingTimeBonus[] _bonusTable;

        public override RemainingTimeBonusRule ToEntity()
        {
            return new RemainingTimeBonusRule(_bonusTable);
        }
    }
}
