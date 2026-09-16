using ShoulderDelivery.Entity;
using System;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>残り時間ボーナスのアセットクラス</summary>
    [CreateAssetMenu(fileName = "RemainingTimeBonus", menuName = "Master/RemainingTimeBonusRuleAsset")]
    public class RemainingTimeBonusRuleAsset : MasterAssetBase<RemainingTimeBonusRule>
    {
        [SerializeField, Tooltip("残り時間に応じたボーナスのテーブル")] RemainingTimeBonusMaster[] _bonusTable;

        public override RemainingTimeBonusRule ToEntity()
        {
            var array = _bonusTable.Select(bonus => bonus.GenerateBonus()).ToArray();
            return new RemainingTimeBonusRule(array);
        }

        /// <summary>アセット用の残り時間ボーナスクラス</summary>
        [Serializable]
        class RemainingTimeBonusMaster
        {
            [SerializeField] int _remainingTime;
            [SerializeField] int _bonus;

            public RemainingTimeBonus GenerateBonus()
            {
                return new RemainingTimeBonus(_remainingTime, _bonus);
            }
        }
    }
}
