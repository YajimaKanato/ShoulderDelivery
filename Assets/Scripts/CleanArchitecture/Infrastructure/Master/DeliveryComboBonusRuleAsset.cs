using ShoulderDelivery.Entity;
using System;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>連続配達成功ボーナスのアセットクラス</summary>
    [CreateAssetMenu(fileName = "DeliveryComboBonus", menuName = "Master/DeliveryComboBonusRuleAsset")]
    public class DeliveryComboBonusRuleAsset : MasterAssetBase<DeliveryComboBonusRule>
    {
        [SerializeField, Tooltip("連続配達成功ボーナスのテーブル")] DeliveryComboBonusMaster[] _bonusTable;

        public override DeliveryComboBonusRule ToEntity()
        {
            var array = _bonusTable.Select(bonus => bonus.GenerateBonus()).ToArray();
            return new DeliveryComboBonusRule(array);
        }

        /// <summary>アセット用の連続配達成功ボーナスクラス</summary>
        [Serializable]
        class DeliveryComboBonusMaster
        {
            [SerializeField, Tooltip("この数値以上という比較")] int _combo;
            [SerializeField, Tooltip("この数値以上になったときにもらえるスコア")] int _bonus;

            public DeliveryComboBonus GenerateBonus()
            {
                return new DeliveryComboBonus(_combo, _bonus);
            }
        }
    }
}
