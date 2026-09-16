using ShoulderDelivery.Entity;
using System;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>投擲時の移動速度ボーナスのアセットクラス</summary>
    [CreateAssetMenu(fileName = "SpeedBonus", menuName = "Master/SpeedBonusRuleAsset")]
    public class SpeedBonusRuleAsset : MasterAssetBase<SpeedBonusRule>
    {
        [SerializeField, Tooltip("投擲時の速度ボーナスのテーブル")] SpeedBonusMaster[] _bonusTable;

        public override SpeedBonusRule ToEntity()
        {
            var array = _bonusTable.Select(bonus => bonus.GenerateBonus()).ToArray();
            return new SpeedBonusRule(array);
        }

        /// <summary>アセット用の移動速度ボーナスクラス</summary>
        [Serializable]
        class SpeedBonusMaster
        {
            [SerializeField] float _speed;
            [SerializeField] int _bonus;

            public SpeedBonus GenerateBonus()
            {
                return new SpeedBonus(_speed, _bonus);
            }
        }
    }
}
