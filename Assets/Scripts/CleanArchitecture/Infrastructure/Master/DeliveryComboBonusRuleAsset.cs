using ShoulderDelivery.Entity;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    [CreateAssetMenu(fileName = "DeliveryComboBonusRuleAsset", menuName = "Master/DeliveryComboBonusRuleAsset")]
    public class DeliveryComboBonusRuleAsset : MasterAssetBase<DeliveryComboBonusRule>
    {
        [SerializeField, Tooltip("連続配達成功ボーナスのテーブル")] DeliveryComboBonus[] _bonusTable;

        public override DeliveryComboBonusRule ToEntity()
        {
            return new DeliveryComboBonusRule(_bonusTable);
        }
    }
}
