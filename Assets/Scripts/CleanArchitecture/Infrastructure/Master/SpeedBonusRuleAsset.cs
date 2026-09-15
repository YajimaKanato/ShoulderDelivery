using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    [CreateAssetMenu(fileName = "SpeedBonusRuleAsset", menuName = "Master/SpeedBonusRuleAsset")]
    public class SpeedBonusRuleAsset : MasterAssetBase<SpeedBonusRule>
    {
        [SerializeField, Tooltip("投擲時の速度ボーナスのテーブル")] SpeedBonus[] _bonusTable;

        public override SpeedBonusRule ToEntity()
        {
            return new SpeedBonusRule(_bonusTable);
        }
    }
}
