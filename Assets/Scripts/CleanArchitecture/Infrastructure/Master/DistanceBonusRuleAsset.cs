using ShoulderDelivery.Entity;
using System;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>距離ボーナスのアセットクラス</summary>
    [CreateAssetMenu(fileName = "DistanceBonus", menuName = "Master/DistanceBonusRuleAsset")]
    public class DistanceBonusRuleAsset : MasterAssetBase<DistanceBonusRule>
    {
        [SerializeField, Tooltip("投擲距離に応じたボーナスのテーブル")] DistanceBonusMaster[] _bonusTable;

        public override DistanceBonusRule ToEntity()
        {
            var array = _bonusTable.Select(bonus => bonus.GenerateBonus()).ToArray();
            return new DistanceBonusRule(array);
        }

        /// <summary>Masterアセット用の距離ボーナスクラス</summary>
        [Serializable]
        class DistanceBonusMaster
        {
            [SerializeField, Tooltip("この数値以上という比較")] float _distance;
            [SerializeField, Tooltip("この数値以上になったときにもらえるスコア")] int _bonus;

            public DistanceBonus GenerateBonus()
            {
                return new DistanceBonus(_distance, _bonus);
            }
        }
    }
}
