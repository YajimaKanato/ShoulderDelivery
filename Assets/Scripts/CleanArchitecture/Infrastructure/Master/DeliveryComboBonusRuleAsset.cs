using ShoulderDelivery.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>連続配達成功ボーナスのアセットクラス</summary>
    [CreateAssetMenu(fileName = "DeliveryComboBonus", menuName = "Master/DeliveryComboBonusRuleAsset")]
    public class DeliveryComboBonusRuleAsset : MasterAssetBase<DeliveryComboBonusRule>
    {
        [SerializeField, Tooltip("配達順に並べた配達対象のインデックス")] int[] _targetIndices;
        [SerializeField, Tooltip("連続配達成功ボーナスのテーブル")] DeliveryComboBonusMaster[] _bonusTable;

        public IReadOnlyList<int> TargetIndices => _targetIndices ?? Array.Empty<int>();

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
